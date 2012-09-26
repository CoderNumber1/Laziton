using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using BlogEngine.Core.DataModels;

namespace BlogEngine.Core.DataContexts
{
    public interface IBlogEngineContext
        : DbContext
    {
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
