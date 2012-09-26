using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlogEngine.Core.DataModels;

namespace BlogEngine.Core.PresentationModels
{
    public class BloggerPModel
    {
        public Blogger BloggerInfo { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
    }
}
