using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Authorizer.Mvc.Security.Session;
using System.Web.Security;

namespace Authorizer.Mvc.Services
{
    public class AuthorizerService
    {
        private static Lazy<AuthorizerService> _Instance = new Lazy<AuthorizerService>(() => new AuthorizerService());
        public static AuthorizerService Service { get { return _Instance.Value; } }
        private AuthorizerService() { }

        public UserSession Session { get { return UserSessions.CurrentSession; } }

        public void StartAuthorizer(HttpApplication App)
        {
            App.PostAuthenticateRequest += new EventHandler(App_PostAuthenticateRequest);
        }

        static void  App_PostAuthenticateRequest(object sender, EventArgs e)
        {
            //UserSessions.LogInUserSession()

            //HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            //if (authCookie != null)
            //{
            //    string encTicket = authCookie.Value;
            //    if (!String.IsNullOrEmpty(encTicket))
            //    {
            //        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(encTicket);
            //        NerdIdentity id = new NerdIdentity(ticket);
            //        GenericPrincipal prin = new GenericPrincipal(id, null);
            //        HttpContext.Current.User = prin;
            //    }
            //}
        }
    }
}