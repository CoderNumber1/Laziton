using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Authorizer.Security.Session;

namespace Authorizer.Security.Attributes
{
    public class RoleAuthAttribute
        : AuthorizeAttribute
    {
        private UserLevels MinimumLevel { get; set; }

        public RoleAuthAttribute(UserLevels minLevel)
        {
            this.MinimumLevel = minLevel;
        }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            return UserSessions.CurrentSession.PermissionLevel >= this.MinimumLevel;
        }
    }
}
