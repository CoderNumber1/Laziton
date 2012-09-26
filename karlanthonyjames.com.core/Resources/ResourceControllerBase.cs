using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using System.IO;

namespace KarlAnthonyJames.Com.Core.Resources
{
    public abstract class ResourceControllerBase
        : Controller
    {
        public abstract string RootNamespace { get; }

        public virtual ActionResult GetResource(string resourceType, string resourceName)
        {
            var contentType = this.GetContentType(resourceName);
            var ResourceAssembly = this.GetResourceAssembly();
            
            var resourceStream = ResourceAssembly.GetManifestResourceStream(string.Format("{0}.{1}.{2}", this.RootNamespace, resourceType, resourceName.Replace("/", ".")));

            if (resourceStream != null)
                return this.File(resourceStream, contentType, resourceName);
            else
                return null;
        }

        protected virtual Assembly GetResourceAssembly()
        {
            return Assembly.GetAssembly(this.GetType());
        }

        protected virtual string GetContentType(string resourceName)
        {
            switch (Path.GetExtension(resourceName))
            {
                case ".js":
                    return "text/javascript";
                case ".css":
                    return "text/css";
                case ".png":
                    return "image/png";
                case ".jpg":
                    return "image/jpeg";
                default:
                    return "text/html";
            }
        }
    }
}
