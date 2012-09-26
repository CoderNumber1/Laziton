using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Reflection;
using KarlAnthonyJames.Com.Core.Resources;

namespace BlogEngineMvc.Controllers
{
    public class ResourceController : ResourceControllerBase
    {
        public override string RootNamespace { get { return "BlogEngineMvc.Content"; } }

        public override ActionResult GetResource(string resourceType, string resourceName)
        {
            return base.GetResource(resourceType, resourceName);
        }
    }
}
