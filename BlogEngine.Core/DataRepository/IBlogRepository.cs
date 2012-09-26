using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlogEngine.Core.DataModels;
using BlogEngine.Core.DataContexts;
using BlogEngine.Core.Configuration;

namespace BlogEngine.Core.DataRepository
{
    public interface IBlogRepository
        : IDisposable
    {
        BlogContext Context { get; }

        IQueryable<Entry> GetEntries();
        IQueryable<Comment> GetComments();
        IQueryable<Blogger> GetBloggers();
        IQueryable<Blog> GetBlogs();

        Blog CreateBlog();
        Blogger CreateBlogger();
        Entry CreateEntry();
        Comment CreateComment();

        void Attach(object entity);
        void Delete(object entity);
        void Save();
    }
}
