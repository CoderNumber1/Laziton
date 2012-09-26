using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogEngine.Core.DataModels
{
    public class Blogger
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Signature { get; set; }
        public string DisplayName { get; set; }
    }
}
