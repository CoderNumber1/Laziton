using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcWebDev.Auth.Security.Session;
using KarlAnthonyJames.Com.Core.Links;

namespace BlogEngineMvc.Controllers
{
    public class BlogConrtollerBase : Controller
    {
        public BlogConrtollerBase()
        {
            if (UserSessions.CurrentSession.PermissionLevel == UserLevels.Admin)
                LinkManager.Instance.SupplementLinks("BlogSubNav");
        }
    }
}
