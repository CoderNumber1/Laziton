using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcContrib.PortableAreas;
using System.Reflection;
using BlogEngine.Core;
using KarlAnthonyJames.Com.Core.Configuration;
using KarlAnthonyJames.Com.Core.Messaging.Messages;
using System.Configuration;
using System.Web.Mvc;

namespace BlogEngineMvc
{
    public class BlogEngineAreaRegistration
        : PortableAreaRegistration
    {
        public override string AreaName
        {
            get { return "Blog"; }
        }

        public override void RegisterArea(System.Web.Mvc.AreaRegistrationContext context, IApplicationBus bus)
        {
            base.RegisterAreaEmbeddedResources();

            context.MapRoute("BlogShortcut"
                , "Blog/Entry/{title}"
                , new { controller = "blog", action = "EntryShortcut", area = "blog" });

            context.MapRoute("BlogEdit"
                , "Blog/Edit/{id}"
                , new { controller = "blogadmin", action = "EditEntry", area = "blog", id = UrlParameter.Optional });

            context.MapRoute("BlogDelete"
                , "Blog/Delete/{id}"
                , new { controller = "blogadmin", action = "DeleteEntry", area = "blog", id = UrlParameter.Optional });

            context.MapRoute("BlogDeleteConfirm"
                , "Blog/ConfirmDelete/{id}"
                , new { controller = "blogadmin", action = "ConfirmDeleteEntry", area = "blog", id = UrlParameter.Optional });

            context.MapRoute("BlogShort"
                , "Blog/{action}"
                , new { controller = "blog", action = "index", area = "blog" });

            context.MapRoute("BlogComment"
                , "Blog/Comment/{id}"
                , new { controller = "blog", action = "comment", area = "blog" });

            context.MapRoute("BlogAdmin"
                , "BlogAdmin/{action}"
                , new { controller = "BlogAdmin", action = "Index", area = "blog" });

            context.MapRoute("BlogResources"
                , "BlogAdmin/Resources/{resourceType}/{resourceName}"
                , new { controller = "Resource", action = "GetResource", area = "blog" });

            var configRequest = new ConfigRequestMessage();
            bus.Send(configRequest);

            if (configRequest.Result.Success)
                (SQLBlogEngine.Engine as IBlogEngine).StartEngine(configRequest.Result.Config);
            else
                (SQLBlogEngine.Engine as IBlogEngine).StartEngine(System.Configuration.ConfigurationManager.AppSettings["BlogEngine_ConfigName"]);
        }
    }
}