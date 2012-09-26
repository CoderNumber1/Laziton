using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using KarlAnthonyJames.Com.Core.Links;

namespace KarlAnthonyJames.Com.Util.Links
{
    public static class LinkHelpers
    {
        public static MvcHtmlString BuildNavigation(this HtmlHelper Helper, string activeLinkName, string linkGroupName)
        {
            TagBuilder LinkContainer = new TagBuilder("ul");
            LinkContainer.AddCssClass("nav nav-pills");

            LinkManager.Instance.GetLinks(linkGroupName)
                .ForEach(Lnk =>
                {
                    string routeName = null;
                    var linkItem = new TagBuilder("li");
                    var RouteParams = new RouteValueDictionary(Lnk.RouteParams);

                    if (!string.IsNullOrEmpty(Lnk.Controller))
                        RouteParams.Add("controller", Lnk.Controller);
                    if (!string.IsNullOrEmpty(Lnk.Action))
                        RouteParams.Add("action", Lnk.Action);
                    if (!string.IsNullOrEmpty(Lnk.Area))
                        RouteParams.Add("area", Lnk.Area);

                    if(Lnk.Name == activeLinkName)
                        linkItem.AddCssClass("active");

                    if (Lnk.UseNamedRoute)
                        routeName = Lnk.RouteName;
                    else
                        routeName = "Default";
                    
                    linkItem.InnerHtml = Helper.RouteLink(Lnk.Text, routeName, RouteParams, Lnk.HtmlAttributes).ToString();

                    LinkContainer.InnerHtml += linkItem.ToString();
                });

            return new MvcHtmlString(LinkContainer.ToString());
        }
    }
}
