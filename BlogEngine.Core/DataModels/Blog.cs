using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogEngine.Core.DataModels
{
    public class Blog
    {
        public int Id { get; set; }
        public string BlogName { get; set; }

        public int BloggerId { get; set; }
        public Blogger Blogger { get; set; }
    }
}
