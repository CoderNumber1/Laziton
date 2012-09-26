using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Runtime.CompilerServices;
using Authorizer.DataModels;
using Authorizer.Services;

namespace Authorizer.Security.Session
{
    public class UserSessions
    {
        private static string SessionKey = "Authorizer.Core.UserSession";

        public static UserSession CurrentSession
        {
            get { return HttpContext.Current.Session[SessionKey] as UserSession; }
        }

        public static void CreateSession()
        {
            HttpContext.Current.Session[SessionKey] = new UserSession()
            {
                DisplayName = "Guest"
                , PermissionLevel = UserLevels.Guest
                , UserLoggedIn = false
            };
        }
    
        public static void LogInUserSession(User user)
        {
            CurrentSession.User = user;
            CurrentSession.DisplayName = user.Alias != null ? user.Alias.Name : user.EmailUserName;
            CurrentSession.PermissionLevel = user.Alias != null ? (UserLevels)user.Alias.PermissionLevel : UserLevels.Guest;

            CurrentSession.UserLoggedIn = true;
            CurrentSession.OpenId = AuthService.Service.Identity.OpenId;
            UserSessions.CurrentSession.PermissionLevel = UserLevels.User;
        }
    }
}
