using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;

namespace KarlAnthonyJames.Com.Util.HtmlHelpers
{
    public static class UrlHelperExtensions
    {
        public static Uri GetBaseUri(this UrlHelper Helper)
        {
            Uri contextUri = new Uri(Helper.RequestContext.HttpContext.Request.Url, Helper.RequestContext.HttpContext.Request.RawUrl);
            UriBuilder realmUri = new UriBuilder(contextUri) { Path = Helper.RequestContext.HttpContext.Request.ApplicationPath, Query = null, Fragment = null };
            return realmUri.Uri;
        }

        public static Uri GetFullUri(this UrlHelper Helper, string requestedUrl)
        {
            Uri ResultUrl = null;

            if (requestedUrl.StartsWith("http:") || requestedUrl.StartsWith("https:"))
            {
                ResultUrl = new Uri(requestedUrl);
            }
            else if (!string.IsNullOrEmpty(HttpContext.Current.Request.ApplicationPath) && HttpContext.Current.Request.ApplicationPath != "/")
            {
                var host = Helper.GetBaseUri();
                ResultUrl = new Uri(string.Format("{0}{1}", host, requestedUrl));
                ResultUrl = new Uri(ResultUrl, ResultUrl.AbsolutePath);
            }
            else
            {
                ResultUrl = new Uri(HttpContext.Current.Request.Url, requestedUrl);
            }

            ResultUrl = new Uri(ResultUrl, ResultUrl.AbsolutePath.Replace("//", "/"));
            return ResultUrl;
        }

        public static string GetFullUrl(this UrlHelper Helper, string requestedUrl)
        {
            return Helper.GetFullUri(requestedUrl).ToString();
        }
    }
}