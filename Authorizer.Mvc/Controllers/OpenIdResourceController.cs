using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KarlAnthonyJames.Com.Core.Resources;
using System.Reflection;

namespace Authorizer.Mvc.Controllers
{
    public class OpenIdResourceController : ResourceControllerBase
    {
        public FileStreamResult GetOpenIdResource(string resourceName)
        {
            var ContentType = base.GetContentType(resourceName);
            var ResourceStream = this.GetResourceAssembly().GetManifestResourceStream(resourceName);
            var Resrouces = this.GetResourceAssembly().GetManifestResourceNames();
            return new FileStreamResult(ResourceStream, ContentType);
        }

        public override ActionResult GetResource(string resourceType, string resourceName)
        {
            return base.GetResource(resourceType, resourceName);
        }

        public override string RootNamespace
        {
            get { return "DotNetOpenAuth.OpenId"; }
        }

        protected override Assembly GetResourceAssembly()
        {
            return typeof(DotNetOpenAuth.OpenId.RelyingParty.OpenIdSelector).Assembly;
        }
    }
}
