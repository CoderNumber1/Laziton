using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlogEngine.Core.DataServices;
using BlogEngine.Core.DataModels;
using BlogEngine.Core.DataRepository;
using BlogEngine.Core.Configuration;

namespace BlogEngine.Core
{
    public class SQLBlogEngine
        : IBlogEngine
    {
        private static Lazy<IBlogEngine> _Engine = new Lazy<IBlogEngine>(() => new SQLBlogEngine());
        public static IBlogEngine Engine { get { return _Engine.Value; } }
        private SQLBlogEngine() { }

        public IBlogEngineConfiguration Config { get; private set; }

        IBlogEngineConfiguration IBlogEngine.Config { get { return this.Config; } }

        void IBlogEngine.StartEngine(string configName)
        {
            this.Config = new BlogEngineConfiguration(configName);
        }

        void IBlogEngine.StartEngine(ConfigurationFramework.Config config)
        {
            this.Config = new BlogEngineConfiguration(config);
        }

        Entry IBlogEngine.GetBlogEntry(int Id)
        {
            using (IBlogService DataService = new BlogEngineDataService(this.Config))
            {
                return DataService.Repository.GetEntries().FirstOrDefault(BlogEntry => BlogEntry.Id == Id);
            }
        }

        Entry IBlogEngine.GetBlogEntry(string title, int blogId)
        {
            using (IBlogService DataService = new BlogEngineDataService(this.Config))
            {
                return DataService.Repository.GetEntries().FirstOrDefault(blogEntry => blogEntry.Title.ToLower() == title.ToLower() && blogEntry.BlogId == blogId && blogEntry.Published);
            }
        }

        void IBlogEngine.AddEntry(DataModels.Entry blogEntry)
        {
            using (IBlogService DataService = new BlogEngineDataService(this.Config))
            {
                DataService.AddBlogEntry(blogEntry);
            }
        }

        void IBlogEngine.UpdateEntry(DataModels.Entry blogEntry)
        {
            using (IBlogService DataService = new BlogEngineDataService(this.Config))
            {
                DataService.UpdateBlogEntry(blogEntry);
            }
        }

        IEnumerable<Comment> IBlogEngine.GetComments(int entryId)
        {
            using (IBlogService DataService = new BlogEngineDataService(this.Config))
            {
                return DataService.GetBlogComments(entryId).OrderBy(comment => comment.CommentDate);
            }
        }

        void IBlogEngine.LeaveComment(Comment comment)
        {
            if (comment.EntryId != 0)
            {
                using (IBlogService DataService = new BlogEngineDataService(this.Config))
                {
                    var Entry = DataService.Repository.GetEntries().Where(BEntry => BEntry.Id == comment.EntryId).FirstOrDefault();
                    if(Entry != null)
                        DataService.AddComment(comment);
                }
            }
        }

        void IBlogEngine.RespondToComment(Comment response)
        {
            if (response.EntryId != 0 && (response.ResponseId != null && response.ResponseId != 0))
            {
                using (IBlogService DataService = new BlogEngineDataService(this.Config))
                {
                    var Entry = DataService.Repository.GetEntries().Where(BEntry => BEntry.Id == response.EntryId).FirstOrDefault();
                    var Comment = DataService.Repository.GetComments().Where(BCom => BCom.Id == response.ResponseId).FirstOrDefault();
                    if (Entry != null && Comment != null)
                        DataService.AddComment(response);
                }
            }
        }


        void IBlogEngine.DeleteEntry(int id)
        {
            using (IBlogService DataService = new BlogEngineDataService(this.Config))
            {
                DataService.RemoveBlogEntry(id);
            }
        }


        bool IBlogEngine.IsBloggerRegistered(string userName)
        {
            using (IBlogService DataService = new BlogEngineDataService(this.Config))
            {
                return DataService.IsBloggerRegistered(userName);
            }
        }

        void IBlogEngine.RegisterBlogger(Blogger blogger)
        {
            using (IBlogService DataService = new BlogEngineDataService(this.Config))
            {
                DataService.RegisterBlogger(blogger);
            }
        }

        PresentationModels.BloggerPModel IBlogEngine.GetBloggerInfo(string userName)
        {
            using (IBlogService DataService = new BlogEngineDataService(this.Config))
            {
                var Blogger = new PresentationModels.BloggerPModel()
                {
                    BloggerInfo = DataService.GetBlogger(userName)
                };

                Blogger.Blogs = DataService.GetBlogs(Blogger.BloggerInfo.Id);

                return Blogger;
            }
        }

        IEnumerable<Entry> IBlogEngine.GetBlogEntries(int blogId)
        {
            using (IBlogService DataService = new BlogEngineDataService(this.Config))
            {
                return DataService.GetBlogEntries(blogId).OrderByDescending(entry => entry.CreateDate);
            }
        }

        IEnumerable<Entry> IBlogEngine.GetBlogEntriesByTag(int blogId, string tag)
        {
            using (IBlogService DataService = new BlogEngineDataService(this.Config))
            {
                return DataService.Repository.GetEntries().Where(entry => entry.BlogId == blogId && entry.Tags.ToUpper().Contains(tag.ToUpper())).ToList();
            }
        }

        IEnumerable<Entry> IBlogEngine.GetNonPublishedEntries(int blogId)
        {
            using (IBlogService DataService = new BlogEngineDataService(this.Config))
            {
                return DataService.GetNonPublishedEntries(blogId).OrderByDescending(entry => entry.CreateDate);
            }
        }

        IEnumerable<Blog> IBlogEngine.GetBlogs(int bloggerId)
        {
            using (IBlogService DataService = new BlogEngineDataService(this.Config))
            {
                return DataService.GetBlogs(bloggerId);
            }
        }

        Blog IBlogEngine.GetBlog(int blogId)
        {
            using (IBlogService DataService = new BlogEngineDataService(this.Config))
            {
                return DataService.Repository.GetBlogs().FirstOrDefault(blog => blog.Id == blogId);
            }
        }

        Blog IBlogEngine.GetBlog(string blogName, int bloggerId)
        {
            using (IBlogService DataService = new BlogEngineDataService(this.Config))
            {
                return DataService.Repository.GetBlogs().FirstOrDefault(blog => blog.BlogName.ToLower() == blogName.ToLower() && blog.BloggerId == bloggerId);
            }
        }

        void IBlogEngine.CreateBlog(Blog blog)
        {
            using (IBlogService DataService = new BlogEngineDataService(this.Config))
            {
                DataService.CreateBlog(blog);
            }
        }

        void IBlogEngine.DeleteBlog(int blogId)
        {
            using (IBlogService DataService = new BlogEngineDataService(this.Config))
            {
                DataService.DeleteBlog(blogId);
            }
        }
    }
}
