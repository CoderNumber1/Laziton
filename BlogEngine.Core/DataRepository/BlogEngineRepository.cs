using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlogEngine.Core.DataContexts;
using BlogEngine.Core.DataModels;
using BlogEngine.Core;
using BlogEngine.Core.Configuration;

namespace BlogEngine.Core.DataRepository
{
    public class BlogEngineRepository
        : IBlogRepository
    {
        public BlogEngineRepository(IBlogEngineConfiguration config)
        {
            this.Context = new BlogContext(config.ContextConnectionString);
        }

        private BlogContext Context { get; set; }

        BlogContext IBlogRepository.Context
        {
            get { return this.Context; }
        }

        IQueryable<Entry> IBlogRepository.GetEntries()
        {
            return this.Context.Entries.AsNoTracking();
        }

        IQueryable<Comment> IBlogRepository.GetComments()
        {
            return this.Context.Comments.AsNoTracking();
        }

        IQueryable<Blogger> IBlogRepository.GetBloggers()
        {
            return this.Context.Bloggers.AsNoTracking();
        }

        IQueryable<Blog> IBlogRepository.GetBlogs()
        {
            return this.Context.Blogs.AsNoTracking();
        }

        Blog IBlogRepository.CreateBlog()
        {
            var Result = new Blog();
            this.Context.Blogs.Add(Result);

            return Result;
        }

        Blogger IBlogRepository.CreateBlogger()
        {
            var Result = new Blogger();
            this.Context.Bloggers.Add(Result);

            return Result;
        }

        Entry IBlogRepository.CreateEntry()
        {
            var Result = new Entry();
            this.Context.Entries.Add(Result);

            return Result;
        }

        Comment IBlogRepository.CreateComment()
        {
            var Result = new Comment();
            this.Context.Comments.Add(Result);

            return Result;
        }

        void IBlogRepository.Attach(object entity)
        {
            this.Context.Entry(entity).State = System.Data.EntityState.Modified;
        }

        void IBlogRepository.Delete(object entity)
        {
            this.Context.Entry(entity).State = System.Data.EntityState.Deleted;
        }

        void IBlogRepository.Save()
        {
            this.Context.SaveChanges();
        }

        #region IDisposable Implementation
        private bool Disposed = false;

        void IDisposable.Dispose()
        {
            this.Dispose(true);
        }

        ~BlogEngineRepository()
        {
            this.Dispose(false);
        }

        private void Dispose(bool dispose)
        {
            if (dispose)
            {
                if (!this.Disposed)
                {
                    this.Context.Dispose();
                    this.Disposed = true;
                }
            }
        }
        #endregion
    }
}
