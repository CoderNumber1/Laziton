using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

namespace Authorizer.Mvc.Security.Session
{
    public class UserSession
    {
        public string UserName { get; internal set; }
        public UserLevels UserPermissionLevel { get; internal set; }
        public string Email { get; internal set; }

        #region Redirects
        public string ReturnAfterAuthUrl { get; set; }
        #endregion
    }
}
