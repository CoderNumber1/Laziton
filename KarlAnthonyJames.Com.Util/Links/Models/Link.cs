using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Web.Routing;

namespace KarlAnthonyJames.Com.Util.Links.Models
{
    public class Link
    {
        public Link(XElement navLink)
        {
            this.HtmlAttributes = new Dictionary<string, object>();
            this.RouteParams = new RouteValueDictionary();

            navLink.Attributes().ToList().ForEach(LnkAttr =>
                {
                    switch (LnkAttr.Name.LocalName)
                    {
                        case "Name":
                            this.Name = LnkAttr.Value.ToString();
                            break;
                        case "UseNamedRoute":
                            this.UseNamedRoute = Convert.ToBoolean(LnkAttr.Value.ToString());
                            break;
                        case "RouteName":
                            this.RouteName = LnkAttr.Value.ToString();
                            break;
                    }
                });
            
            navLink.Elements().ToList().ForEach(LnkElem =>
                {
                    switch (LnkElem.Name.LocalName)
                    {
                        case "Text":
                            this.Text = LnkElem.Value;
                            break;
                        case "Controller":
                            this.Controller = LnkElem.Value;
                            break;
                        case "Action":
                            this.Action = LnkElem.Value;
                            break;
                        case "Area":
                            this.Area = LnkElem.Value;
                            break;
                        case "HtmlAttribute":
                            this.HtmlAttributes.Add(LnkElem.Attribute("Name").Value, LnkElem.Attribute("Value").Value);
                            break;
                        case "RouteParam":
                            this.RouteParams.Add(LnkElem.Attribute("Name").Value, LnkElem.Attribute("Value").Value);
                            break;
                    }
                });
        }

        public string Name { get; set; }
        public string RouteName { get; set; }
        public string Text { get; set; }

        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

        public bool UseNamedRoute { get; set; }

        public RouteValueDictionary RouteParams { get; set; }
        public Dictionary<string, object> HtmlAttributes { get; set; }
    }
}
