using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Mvc.Razor;

namespace Authorizer.Mvc.Helpers
{
    public enum CommonResources { Jquery, None }
    public enum BaseType{Jquery}

    public static class ResourceExtensions
    {
        public static MvcHtmlString GetAuthorizerScripts(this HtmlHelper helper, BaseType libraryBase, CommonResources includedResources)
        {
            UrlHelper Urls = new UrlHelper(helper.ViewContext.RequestContext);
            List<TagBuilder> LibrarySupportingElements = new List<TagBuilder>();

            TagBuilder OpenIdLibrary = new TagBuilder("script");
            OpenIdLibrary.Attributes.Add(new KeyValuePair<string,string>( "type", "text/javascript"));
            
            switch(libraryBase)
            {
                case BaseType.Jquery:
                    OpenIdLibrary.Attributes.Add(new KeyValuePair<string, string>("src", Urls.RouteUrl("AuthorizationResources", new {resourceType = "Scripts", resourceName = "openid-jquery.js"})));
                    TagBuilder LanguageFile = new TagBuilder("script");
                    LanguageFile.Attributes.Add(new KeyValuePair<string, string>("type", "text/javascript"));
                    LanguageFile.Attributes.Add(new KeyValuePair<string, string>("src", Urls.RouteUrl("AuthorizationResources", new { resourceType = "Scripts", resourceName = "openid-en.js" })));

                    LibrarySupportingElements.Add(LanguageFile);
                    break;
                default:
                    throw new InvalidOperationException();
            }

            string RawResult = OpenIdLibrary.ToString(TagRenderMode.Normal);
            LibrarySupportingElements.ForEach(Lib => RawResult += Lib.ToString(TagRenderMode.Normal));
            return MvcHtmlString.Create(RawResult);
        }

        public static MvcHtmlString GetAuthorizerStyles(this HtmlHelper helper)
        {
            UrlHelper Urls = new UrlHelper(helper.ViewContext.RequestContext);

            List<TagBuilder> OpenIdStyles = new List<TagBuilder>();

            TagBuilder OpenIdCss = new TagBuilder("link");
            OpenIdCss.Attributes.Add(new KeyValuePair<string, string>("type", "text/css"));
            OpenIdCss.Attributes.Add(new KeyValuePair<string, string>("rel", "stylesheet"));
            OpenIdCss.Attributes.Add(new KeyValuePair<string, string>("href", Urls.RouteUrl("AuthorizationResources", new { resourceType = "Styles", resourceName = "openid.css" })));

            OpenIdStyles.Add(OpenIdCss);
            
            TagBuilder OpenIdShadowCss = new TagBuilder("link");
            OpenIdShadowCss.Attributes.Add(new KeyValuePair<string, string>("type", "text/css"));
            OpenIdShadowCss.Attributes.Add(new KeyValuePair<string, string>("rel", "stylesheet"));
            OpenIdShadowCss.Attributes.Add(new KeyValuePair<string, string>("href", Urls.RouteUrl("AuthorizationResources", new { resourceType = "Styles", resourceName = "openid-shadow.css" })));

            OpenIdStyles.Add(OpenIdShadowCss);

            return MvcHtmlString.Create(string.Join(string.Empty, OpenIdStyles.Select(Style => Style.ToString(TagRenderMode.SelfClosing))));
        }
    }
}