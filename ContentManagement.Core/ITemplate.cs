using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContentManagement
{
    public interface ITemplate : ISubSection
    {
        IRegion NorthRegion { get; set; }
        IRegion SouthRegion { get; set; }
        IRegion WestRegion { get; set; }
        IRegion EastRegion { get; set; }
        IRegion CenterRegion { get; set; }
    }

    public interface ITemplateMetadata
    {
        Dictionary<string, string> RawProperties { get; set; }

        String Path { get; set; }
        String Name { get; set; }

        bool Enabled { get; set; }
    }

    public interface IRegion : ISubSection
    {
        public Dictionary<string, ISubSection> SubSections { get; set; }
    }

    public interface ISubSection
    {
        ITemplateMetadata Metadata { get; set; }
    }
}
