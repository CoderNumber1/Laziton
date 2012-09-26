using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcContrib.PortableAreas;

namespace Authorizer.Mvc
{
    public class AuthorizationAreaRegistration
        : PortableAreaRegistration
    {
        public override string AreaName
        {
            get { return "Authorization"; }
        }

        public override void RegisterArea(System.Web.Mvc.AreaRegistrationContext context, IApplicationBus bus)
        {
            base.RegisterArea(context, bus);

            context.MapRoute("Authorization"
                , "Auth/{action}"
                , new { controller = "Auth", action = "Index" });

            context.MapRoute("AuthorizationResources"
                , "AuthResource/{resourceType}/{resourceName}"
                , new { controller = "AuthResource", action = "GetResource" });

            context.MapRoute("EmbeddedOpenIdResources"
                , "EmbededOpenIdResources/{resourceName}"
                , new { controller = "OpenIdResource", action = "GetOpenIdResource" });

            context.MapRoute("OpenIdResources"
                , "OpenIdResources/{resourceType}/{resourceName}"
                , new { controller = "OpenIdResource", action = "GetResource" });

            context.MapRoute(
                "OpenIdDiscover",
                "Auth/Discover"
            );

            
        }
    }
}