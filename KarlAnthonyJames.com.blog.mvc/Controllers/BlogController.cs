using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogEngine.Core;
using BlogEngine.Core.DataModels;
using BlogEngineMvc.ViewModels;
using KarlAnthonyJames.Com.Core.Configuration;
using MvcWebDev.Auth.Security.Session;
using KarlAnthonyJames.Com.Core.Links;
using KarlAnthonyJames.Com.Core.Profiles;
using GravatarHelper.Extensions;

namespace BlogEngineMvc.Controllers
{
    [AllowAnonymous]
    public class BlogController : JSONGetsController
    {
        public ActionResult Index(int? id)
        {
            ViewBag.EntryId = id != null ? id.Value : 0;
            return View("Index");
        }

        public ActionResult EntryShortcut(string title)
        {
            int? EntryId = null;
            var Entry = base.BlogEngine.GetBlogEntry(title, CoreConfiguration.Instance.BlogId);

            if (Entry != null)
                EntryId = Entry.Id;

            return Index(EntryId);
        }

        //public JsonResult GetJSONEntries()
        //{
        //    var Entries = BlogEngine.GetBlogEntries(CoreConfiguration.Instance.BlogId).ToList();
        //    return new JsonResult() { Data = Entries, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}

        //public JsonResult GetJSONEntry(int Id)
        //{
        //    var Entry = BlogEngine.GetBlogEntry(Id);
        //    return new JsonResult() { Data = Entry, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}

        //public JsonResult GetJSONComments(int entryId)
        //{
        //    UrlHelper helper = new UrlHelper(Request.RequestContext);
        //    var Comments = BlogEngine.GetComments(entryId);

        //    Comments.ToList().ForEach(comment =>
        //        {
        //            comment.ByEmail = helper.Gravatar(SiteProfile.GetProfileEmail(comment.ByUserId), 40, "identicon", GravatarHelper.GravatarRating.PG);
        //        });

        //    return new JsonResult() { Data = Comments, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //}

        public void Comment(int entryId, string comment)
        {
            Comment EntryComment = new Comment();

            EntryComment.CommentDate = DateTime.Now;
            EntryComment.Content = comment;
            EntryComment.EntryId = entryId;
            EntryComment.CanRespond = true;

            if (Request.IsAuthenticated)
            {
                SiteProfile Profile = SiteProfile.GetProfile(User.Identity.Name);

                EntryComment.ByUserId = User.Identity.Name;
                EntryComment.By = User.Identity.Name;
                EntryComment.ByEmail = Profile.Email; 
            }
            else
            {
                EntryComment.By = "Anonymous";
            }

            BlogEngine.LeaveComment(EntryComment);
        }

    }
}
