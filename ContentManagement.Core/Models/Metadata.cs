using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContentManagement.Models
{
    public class Metadata
    {
        public int Id { get; set; }

        public string Path { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }

        public virtual ICollection<MetadataItem> MetadataItems { get; set; }
    }

    public class MetadataItem
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public int MetadataId { get; set; }
    }
}
