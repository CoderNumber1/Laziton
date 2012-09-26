using System;
using System.Web;
using DotNetOpenAuth;
using System.Web.Routing;
using System.Web.Mvc;
using KarlAnthonyJames.Com.Util.HtmlHelpers;

namespace Authorizer.Services
{
    public class EmbeddedResourceUrlService : IEmbeddedResourceRetrieval
    {
        private static string pathFormat = "/EmbededOpenIdResources/{0}";

        public Uri GetWebResourceUrl(Type someTypeInResourceAssembly, string manifestResourceName)
        {
            if (manifestResourceName.Contains("http"))
            {
                return new Uri(manifestResourceName);
            }
            else
            {
                UrlHelper Url = new UrlHelper(HttpContext.Current.Request.RequestContext);
                var path = string.Format(pathFormat,
                            HttpUtility.UrlEncode(manifestResourceName));

                return Url.GetFullUri(path);
            }
        }
    }
}