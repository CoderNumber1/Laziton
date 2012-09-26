using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Authorizer.Security.Session;
using System.Web;
using System.Web.Security;
using Authorizer.DataServices;

namespace Authorizer.Security.Attributes
{
    public class AuthAttribute
        : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.User.Identity.IsAuthenticated && (filterContext.RequestContext.HttpContext.User.Identity as AuthIdentity).OpenId != UserSessions.CurrentSession.OpenId)
            {
                //User is falsely authenticated, need to log them out and return them to the login page.
            }

            if (UserSessions.CurrentSession.UserLoggedIn && UserSessions.CurrentSession.PermissionLevel == UserLevels.Guest && filterContext.ActionDescriptor.ActionName != "RegisterAlias")
            {
                UserSessions.CurrentSession.RedirectToUrl = filterContext.RequestContext.HttpContext.Request.RawUrl;
                filterContext.Result = new RedirectToRouteResult("Authorization", new System.Web.Routing.RouteValueDictionary(new { action = "RegisterAlias" }));
            }
        }
    }
}
