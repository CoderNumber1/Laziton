using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Authorizer.Mvc.Security.Session;

namespace Authorizer.Mvc.Security.Attributes
{
    public class RoleLevelAuthorizationAttribute
        : AuthorizeAttribute
    {
        private UserLevels MinimumLevel { get; set; }

        public RoleLevelAuthorizationAttribute(UserLevels minLevel)
        {
            this.MinimumLevel = minLevel;
        }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            return UserSessions.CurrentSession.UserPermissionLevel >= this.MinimumLevel;
        }
    }
}
