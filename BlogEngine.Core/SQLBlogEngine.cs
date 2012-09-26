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


        bool IBlogEngine.IsBloggerRegistered(int userId)
        {
            using (IBlogService DataService = new BlogEngineDataService(this.Config))
            {
                return DataService.IsBloggerRegistered(userId);
            }
        }

        void IBlogEngine.RegisterBlogger(Blogger blogger)
        {
            using (IBlogService DataService = new BlogEngineDataService(this.Config))
            {
                DataService.RegisterBlogger(blogger);
            }
        }

        PresentationModels.BloggerPModel IBlogEngine.GetBloggerInfo(int userId)
        {
            using (IBlogService DataService = new BlogEngineDataService(this.Config))
            {
                var Blogger = new PresentationModels.BloggerPModel()
                {
                    BloggerInfo = DataService.GetBlogger(userId)
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
    }
}
