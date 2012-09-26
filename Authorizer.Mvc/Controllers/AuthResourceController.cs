using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Reflection;
using KarlAnthonyJames.Com.Core.Resources;

namespace Authorizer.Mvc.Controllers
{
    public class AuthResourceController : ResourceControllerBase 
    {
        public override string RootNamespace { get { return "Authorizer.Mvc.Content"; } }
    }
}
