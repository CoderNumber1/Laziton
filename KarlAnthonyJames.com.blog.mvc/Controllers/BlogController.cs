using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogEngine.Core;
using BlogEngineMvc.ViewModels;
using KarlAnthonyJames.Com.Core.Configuration;
using MvcWebDev.Auth.Security.Session;
using KarlAnthonyJames.Com.Core.Links;

namespace BlogEngineMvc.Controllers
{
    public class BlogController : BlogConrtollerBase
    {
        private IBlogEngine BlogEngine { get { return SQLBlogEngine.Engine; } }

        public ActionResult Index()
        {
            //var Entries = BlogEngine.GetBlogEntries(CoreConfiguration.Instance.BlogId).ToList();
            return View();
        }

        public JsonResult GetJSONEntries()
        {
            var Entries = BlogEngine.GetBlogEntries(CoreConfiguration.Instance.BlogId).ToList();
            return new JsonResult() { Data = Entries, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetJSONEntry(int Id)
        {
            var Entry = BlogEngine.GetBlogEntry(Id);
            return new JsonResult() { Data = Entry, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetJSONComments(int entryId)
        {
            var Comments = BlogEngine.GetComments(entryId);
            return new JsonResult() { Data = Comments, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
