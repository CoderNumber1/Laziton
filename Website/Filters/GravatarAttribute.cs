using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GravatarHelper.Extensions;
using KarlAnthonyJames.Com.Core.Profiles;

namespace Website.Filters
{
    public class GravatarAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            UrlHelper Helper = new UrlHelper(filterContext.RequestContext);
            filterContext.Controller.ViewBag.GravatarUrl = Helper.Gravatar(SiteProfile.GetProfileEmail(filterContext.HttpContext.User.Identity.Name), 30, "identicon", GravatarHelper.GravatarRating.PG);
        }
    }
}