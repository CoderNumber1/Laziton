using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Runtime.CompilerServices;

namespace Authorizer.Mvc.Security.Session
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
                UserName = "guest"
                , UserPermissionLevel = UserLevels.Guest
                , Email = null
            };
        }



        public static void LogInUserSession(string userName, UserLevels userLevel, string email)
        {
            CurrentSession.UserName = userName;
            CurrentSession.UserPermissionLevel = userLevel;
            CurrentSession.Email = email;
        }
    }
}
