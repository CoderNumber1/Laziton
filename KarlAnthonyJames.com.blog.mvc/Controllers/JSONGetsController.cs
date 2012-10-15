using BlogEngine.Core;
using KarlAnthonyJames.Com.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GravatarHelper.Extensions;
using KarlAnthonyJames.Com.Core.Profiles;

namespace BlogEngineMvc.Controllers
{
    public class JSONGetsController : BlogConrtollerBase
    {
        protected IBlogEngine BlogEngine { get { return SQLBlogEngine.Engine; } }

        public virtual JsonResult GetJSONEntries()
        {
            var Entries = BlogEngine.GetBlogEntries(CoreConfiguration.Instance.BlogId).ToList();
            return new JsonResult() { Data = Entries, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public virtual JsonResult GetJSONEntry(int Id)
        {
            var Entry = BlogEngine.GetBlogEntry(Id);
            return new JsonResult() { Data = Entry, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public virtual JsonResult GetJSONComments(int entryId)
        {
            UrlHelper helper = new UrlHelper(Request.RequestContext);
            var Comments = BlogEngine.GetComments(entryId);

            Comments.ToList().ForEach(comment =>
            {
                comment.ByEmail = helper.Gravatar(SiteProfile.GetProfileEmail(comment.ByUserId), 40, "identicon", GravatarHelper.GravatarRating.PG);
            });

            return new JsonResult() { Data = Comments, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
