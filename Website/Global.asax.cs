using KarlAnthonyJames.Com.Core.Configuration;
using KarlAnthonyJames.Com.Core.Links;
using MvcWebDev.Auth.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Website
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            MvcContrib.PortableAreas.PortableAreaRegistration.RegisterAllAreas();
            MvcContrib.PortableAreas.PortableAreaRegistration.RegisterEmbeddedViewEngine();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            LinkManager.Instance.Instantiate(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CoreConfiguration.Instance.NavPath));
            //AuthService.Service.StartService(CoreConfiguration.Instance.AuthorizerConnectionString);

        }
    }
}