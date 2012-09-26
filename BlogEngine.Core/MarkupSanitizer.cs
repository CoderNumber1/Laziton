using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace BlogEngine.Core
{
    public class MarkupSanitizer
    {
        public string Sanitize(string markup)
        {
            if (markup == null)
                return null;

            string Result = string.Empty;

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(markup);


            string[] elementWhitelist = {
                                            "a", "u", "b", "i", "br", "br ", "br", "h1", "h2", "h3", "h4", "h5", "h6", "span",
                                            "div", "blockquote", "em", "sub", "sup", "s", "font", "ul", "li", "ol", "p", "#text", "img"
                                        };

            string[] attributeWhiteList = { "class", "style", "src", "href", "color", "size" };

            IList<HtmlNode> hnc = doc.DocumentNode.Descendants().ToList();



            //remove non-white list nodes
            for (int i = hnc.Count - 1; i >= 0; i--)
            {
                HtmlNode htmlNode = hnc[i];
                if (!elementWhitelist.Contains(htmlNode.Name.ToLower()))
                {
                    htmlNode.Remove();
                    continue;
                }

                for (int att = htmlNode.Attributes.Count - 1; att >= 0; att--)
                {
                    HtmlAttribute attribute = htmlNode.Attributes[att];
                    //remove any attribute that is not in the white list (such as event handlers)
                    if (!attributeWhiteList.Contains(attribute.Name.ToLower()))
                    {
                        attribute.Remove();
                    }

                    //strip any "style" attributes that contain the word "expression"
                    if (attribute.Value.ToLower().Contains("expression") && attribute.Name.ToLower() == "style")
                    {
                        attribute.Value = string.Empty;
                    }


                    if (attribute.Name.ToLower() == "src" || attribute.Name.ToLower() == "href")
                    {
                        //strip if the link starts with anything other than http (such as jscript, javascript, vbscript, mailto, ftp, etc...)
                        if (!attribute.Value.StartsWith("http")) attribute.Value = "#";
                    }
                }
            }
            return doc.DocumentNode.WriteTo();
        }
    }
}
