using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;

using KarlAnthonyJames.Com.Core.Configuration;

using KarlAnthonyJames.Com.Util.Links.Models;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;

namespace KarlAnthonyJames.Com.Util.Links
{
    public class LinkManager
    {
        private static Lazy<LinkManager> _Manager = new Lazy<LinkManager>(() => new LinkManager());
        public static LinkManager Instance { get { return _Manager.Value; } }
        private LinkManager() { }

        public void Instantiate(string path)
        {
            RouteTable.Routes.MapRoute(this.RouteResetName
                , "{LinkManagerRoute}/{WontMap}");

            this.LoadNavigationStructure(path);
        }

        public void LoadNavigationStructure(string path)
        {
            this.LinksCollection = XDocument.Load(path)
                .Descendants("NavGroup")
                .ToDictionary(NavGroup => NavGroup.Attribute("Name").Value, NavGroup => new LinkGroup(NavGroup));
        }

        private Dictionary<string, LinkGroup> LinksCollection { get; set; }
        public string RouteResetName { get { return "LnkMgrRte"; } }
        public string MainNavigationGroup { get; set; }

        public List<Link> GetLinks(string collectionName)
        {
            return this.LinksCollection[collectionName].Links;
        }

        public Link GetLink(string collectionName, string linkName)
        {
            return this.LinksCollection[collectionName][linkName];
        }
    }
}
