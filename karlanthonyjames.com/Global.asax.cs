using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using BlogEngine;
using BlogEngine.Core.DataContexts;
using KarlAnthonyJames.Com.Core.Configuration;
using System.IO;
using System.Reflection;
using KarlAnthonyJames.Com.Core.Links;
using KarlAnthonyJames.Com.Core.Routing;
using System.Diagnostics;
//using Authorizer.Mvc.Security.Session;
using BlogEngine.Core.Configuration;
using MvcWebDev.Auth.Services;
using MvcWebDev.Auth.Security.Session;

namespace KarlAnthonyJames.Com
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new KarlAnthonyJames.Com.Util.ExceptionHandlingTest());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                //, new { CanRender = new RouteIgnoreConstraint() } // Parameter defaults
            );
        }

        public override void Init()
        {
            AuthService.Service.RequestInit(this);

            base.Init();
        }

        protected void Application_Start()
        {
            MvcContrib.PortableAreas.PortableAreaRegistration.RegisterAllAreas();
            MvcContrib.PortableAreas.PortableAreaRegistration.RegisterEmbeddedViewEngine();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            LinkManager.Instance.Instantiate(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CoreConfiguration.Instance.NavPath));
            AuthService.Service.StartService(CoreConfiguration.Instance.AuthorizerConnectionString);

            //Database.SetInitializer<BlogContext>(new System.Data.Entity.DropCreateDatabaseAlways<BlogContext>());
            //using (var Context = new BlogContext((new BlogEngineConfiguration(CoreConfiguration.Instance.Config) as IBlogEngineConfiguration).ContextConnectionString))
            //{
            //    Context.Database.Initialize(force: true);
            //}

            //Database.SetInitializer<AuthorizerContext>(new AuthorizerInitializer());
            //using (var AuthContext = new AuthorizerContext(CoreConfiguration.Instance.AuthorizerConnectionString))
            //{
            //    AuthContext.Database.Initialize(force: true);
            //}
        }

        public void Session_Start()
        {
            UserSessions.CreateSession();
        }
    }
}