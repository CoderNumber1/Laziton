using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContentManagement.Models
{
    public class Page
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ContentModeId { get; set; }
    }
}
