using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;

using KarlAnthonyJames.Com.Core.Configuration;
using KarlAnthonyJames.Com.Core.Links.Models;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;

namespace KarlAnthonyJames.Com.Core.Links
{
    public class LinkManager
    {
        private const string SUPPLEMENT_LINK_KEY = "SupplementLinkGroups";
        private static Lazy<LinkManager> _Manager = new Lazy<LinkManager>(() => new LinkManager());
        public static LinkManager Instance { get { return _Manager.Value; } }
        private LinkManager() { }

        public void Instantiate(string path)
        {
            this.LoadNavigationStructure(path);
        }

        public void LoadNavigationStructure(string path)
        {
            this.LinksCollection = XDocument.Load(path)
                .Descendants("NavGroup")
                .ToDictionary(NavGroup => NavGroup.Attribute("Name").Value, NavGroup => new LinkGroup(NavGroup));
        }

        private Dictionary<string, LinkGroup> LinksCollection { get; set; }
        public string MainNavigationGroup { get; set; }

        public List<Link> GetLinks(string collectionName)
        {
            return this.GetLinks(collectionName, true);
        }

        public List<Link> GetLinks(string collectionName, bool loadSupplements)
        {
            var ResultLinks = new List<Link>(this.LinksCollection[collectionName].Links);

            if (loadSupplements && HttpContext.Current.Items.Contains(SUPPLEMENT_LINK_KEY))
            {
                (HttpContext.Current.Items[SUPPLEMENT_LINK_KEY] as List<string>).ForEach(group =>
                {
                    ResultLinks.AddRange(this.GetLinks(group, false));
                });
            }
            return ResultLinks;
        }

        public void SupplementLinks(string collectionName)
        {
            if (!HttpContext.Current.Items.Contains(SUPPLEMENT_LINK_KEY))
                HttpContext.Current.Items.Add(SUPPLEMENT_LINK_KEY, new List<string>());

            (HttpContext.Current.Items[SUPPLEMENT_LINK_KEY] as List<string>).Add(collectionName);
        }
        
        public Link GetLink(string collectionName, string linkName)
        {
            return this.LinksCollection[collectionName][linkName];
        }
    }
}
