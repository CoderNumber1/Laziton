using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Collections.Specialized;

namespace KarlAnthonyJames.Com.Core.Links.Models
{
    public class LinkGroup
    {
        public LinkGroup(XElement navGroup)
        {
            this.Links = navGroup
                .Descendants("Link")
                .Select(Link => new Link(Link))
                .ToList();
        }

        public List<Link> Links { get; set; }

        public Link this[string linkName]
        {
            get { return this.Links.Where(Link => Link.Name == linkName).FirstOrDefault(); }
        }
    }
}
