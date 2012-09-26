using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Authorizer.Security.Session;
using System.Web.Security;
using Authorizer.DataServices;
using Authorizer.Security;
using System.Security.Principal;
using System.Web.SessionState;
using Authorizer.Security.Attributes;
using System.Diagnostics;
using Authorizer.Configuration;

namespace Authorizer.Services
{
    public class AuthService : IRequiresSessionState
    {
        private static Lazy<AuthService> _Instance = new Lazy<AuthService>(() => new AuthService());
        public static AuthService Service { get { return _Instance.Value; } }
        private AuthService() { }

        public AuthIdentity Identity { get { return (HttpContext.Current.User.Identity as AuthIdentity); } }
        //public Uri HomeUri { get; set; }

        public void StartService()
        {
            System.Web.Mvc.GlobalFilters.Filters.Add(new AuthAttribute());
        }

        public void RequestInit(HttpApplication App)
        {
            App.PostAuthenticateRequest += new EventHandler(App_PostAuthenticateRequest);
            App.PreRequestHandlerExecute += new EventHandler(App_PreRequestHandlerExecute);
        }

        public FormsAuthenticationTicket CreateAuthTicket(string name, string userData)
        {
            return new FormsAuthenticationTicket(1
                            , name
                            , DateTime.Now
                            , DateTime.Now.AddMinutes(30)
                            , false
                            , userData);
        }

        public void UpdateDisplayName()
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                string encTicket = authCookie.Value;
                if (!String.IsNullOrEmpty(encTicket))
                {
                    FormsAuthenticationTicket Ticket = FormsAuthentication.Decrypt(encTicket);
                    if (UserSessions.CurrentSession.UserLoggedIn && UserSessions.CurrentSession.User.Alias != null)
                    {
                        Ticket = this.CreateAuthTicket(Ticket.Name, UserSessions.CurrentSession.User.Alias.Name);
                        string EncTicket = FormsAuthentication.Encrypt(Ticket);

                        FormsAuthentication.SignOut();
                        HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, EncTicket));
                        UpdateRequestIdentity();
                    }
                }
            }
        }

        private static void UpdateRequestIdentity()
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                string encTicket = authCookie.Value;
                if (!String.IsNullOrEmpty(encTicket))
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(encTicket);
                    AuthIdentity id = new AuthIdentity(ticket);
                    GenericPrincipal prin = new GenericPrincipal(id, null);
                    HttpContext.Current.User = prin;
                }
            }
        }

        void App_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                string encTicket = authCookie.Value;
                if (!String.IsNullOrEmpty(encTicket))
                {
                    FormsAuthenticationTicket Ticket = FormsAuthentication.Decrypt(encTicket);
                    if (HttpContext.Current.Session != null && !UserSessions.CurrentSession.UserLoggedIn)
                    {
                        using (IAuthorizerDataService DataService = DataServiceProvider.Instance.CreateAuthorizerDataService())
                        {
                            UserSessions.LogInUserSession(DataService.RetrieveUserInfo(Ticket.Name));

                            if (UserSessions.CurrentSession.User.Alias != null)
                            {
                                UpdateDisplayName();
                            }
                        }
                    }
                }
            }
        }

        static void  App_PostAuthenticateRequest(object sender, EventArgs e)
        {
            UpdateRequestIdentity();   
        }
    }
}