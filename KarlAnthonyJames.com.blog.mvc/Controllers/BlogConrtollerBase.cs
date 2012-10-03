using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcWebDev.Auth.Security.Session;
using KarlAnthonyJames.Com.Core.Links;

namespace BlogEngineMvc.Controllers
{
    [AdminNav]
    public class BlogConrtollerBase : Controller
    {
        public BlogConrtollerBase()
        {
            //if(User.IsInRole("BlogAdmin"))
            ////if (UserSessions.CurrentSession.PermissionLevel == UserLevels.Admin)
            //    LinkManager.Instance.SupplementLinks("BlogSubNav");
        }
    }

    public class AdminNavAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Request.IsAuthenticated && HttpContext.Current.User.IsInRole("BlogAdmin"))
                LinkManager.Instance.SupplementLinks("BlogSubNav");

            base.OnActionExecuting(filterContext);
        }
    }
}
