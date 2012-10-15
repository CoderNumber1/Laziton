using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlogEngine.Core.DataRepository;
using BlogEngine.Core.Configuration;

namespace BlogEngine.Core.DataServices
{
    public class BlogEngineDataService
        : IBlogService
    {
        #region IDisposable Implementation
        private bool Disposed = false;
        void IDisposable.Dispose()
        {
            this.Dispose(true);
        }

        ~BlogEngineDataService()
        {
            this.Dispose(false);
        }

        private void Dispose(bool dispose)
        {
            if (!this.Disposed)
            {
                if (dispose)
                {
                    this.Repository.Dispose();
                    this.Disposed = true;
                }
            }
        }
        #endregion

        private IBlogRepository Repository { get; set; }

        DataRepository.IBlogRepository IBlogService.Repository
        {
            get { return this.Repository; }
        }

        public BlogEngineDataService(IBlogEngineConfiguration config)
        {
            this.Repository = new BlogEngineRepository(config);
        }

        IEnumerable<DataModels.Entry> IBlogService.GetBlogEntries(int blogId, DateTime entryDate, bool? assending)
        {
            if (assending.Value)
            {
                return this.Repository.GetEntries().Where(Entry => Entry.BlogId == blogId && Entry.Published && Entry.CreateDate >= entryDate).ToList();
            }
            else
            {
                return this.Repository.GetEntries().Where(Entry => Entry.BlogId == blogId && Entry.Published && Entry.CreateDate <= entryDate).ToList();
            }
        }

        IEnumerable<DataModels.Comment> IBlogService.GetBlogComments(int entryID)
        {
            return this.Repository.GetComments().Where(Comment => Comment.EntryId == entryID).ToList();
        }

        IEnumerable<DataModels.Comment> IBlogService.GetBlogCommentResponses(int commentID)
        {
            return this.Repository.GetComments().Where(Comment => Comment.ResponseId == commentID);
        }

        void IBlogService.AddBlogEntry(DataModels.Entry entry)
        {
            var Blog = this.Repository.GetBlogs().FirstOrDefault(b => b.Id == entry.BlogId);

            if (Blog != null)
            {
                var Entry = this.Repository.GetEntries().FirstOrDefault(e => e.Id == entry.Id && e.BlogId == entry.BlogId);

                if (Entry != null)
                {
                    this.Repository.Attach(entry);
                }
                else
                {
                    Entry = this.Repository.CreateEntry();
                }

                Entry.BlogId = entry.BlogId;
                Entry.CreateDate = entry.CreateDate;
                Entry.EditedDate = entry.EditedDate;
                Entry.EntryText = entry.EntryText;
                Entry.IsRawHtml = entry.IsRawHtml;
                Entry.Published = entry.Published;
                Entry.Tags = entry.Tags;
                Entry.Title = entry.Title;

                this.Repository.Save();
            }
        }

        void IBlogService.UpdateBlogEntry(DataModels.Entry entry)
        {
            (this as IBlogService).AddBlogEntry(entry);
        }

        void IBlogService.RemoveBlogEntry(DataModels.Entry entry)
        {
            (this as IBlogService).RemoveBlogEntry(entry.Id);
        }

        void IBlogService.RemoveBlogEntry(int entryID)
        {
            var Entry = this.Repository.GetEntries().FirstOrDefault(e => e.Id == entryID);

            if (Entry != null)
            {
                this.Repository.Delete(Entry);
                this.Repository.Save();
            }
        }

        void IBlogService.AddComment(DataModels.Comment comment)
        {
            var Comment = this.Repository.GetComments().FirstOrDefault(c => c.Id == comment.Id);

            if (Comment == null)
            {
                Comment = this.Repository.CreateComment();

                Comment.By = comment.By;
                Comment.ByEmail = comment.ByEmail;
                Comment.ByUserId = comment.ByUserId;
                Comment.CanRespond = comment.CanRespond;
                Comment.CommentDate = comment.CommentDate;
                Comment.Content = comment.Content;
                Comment.EntryId = comment.EntryId;

                this.Repository.Save();
            }
        }

        void IBlogService.RemoveComment(DataModels.Comment comment)
        {
            (this as IBlogService).RemoveComment(comment.Id);
        }

        void IBlogService.RemoveComment(int commentID)
        {
            var Comment = this.Repository.GetComments().FirstOrDefault(c => c.Id == commentID);

            if (Comment != null)
            {
                this.Repository.Delete(Comment);
                this.Repository.Save();
            }
        }

        bool IBlogService.IsBloggerRegistered(string UserName)
        {
            var Blogger = this.Repository.GetBloggers().FirstOrDefault(b => b.UserName.ToUpper() == UserName.ToUpper());

            return Blogger != null;
        }

        void IBlogService.RegisterBlogger(DataModels.Blogger blogger)
        {
            var Blogger = this.Repository.GetBloggers().FirstOrDefault(b => b.Id == blogger.Id && b.UserId == blogger.UserId);

            if (Blogger == null)
            {
                Blogger = this.Repository.CreateBlogger();

                Blogger.UserName = blogger.UserName;
                Blogger.DisplayName = blogger.DisplayName;
                Blogger.Signature = blogger.Signature;
                Blogger.UserId = blogger.UserId;

                this.Repository.Save();
            }
        }

        DataModels.Blogger IBlogService.GetBlogger(string userName)
        {
            var Blogger = this.Repository.GetBloggers().FirstOrDefault(b => b.UserName.ToUpper() == userName.ToUpper());

            return Blogger;
        }

        IEnumerable<DataModels.Blog> IBlogService.GetBlogs(int bloggerId)
        {
            var Blogs = this.Repository.GetBlogs().Where(blog => blog.BloggerId == bloggerId).ToList();

            return Blogs;
        }

        void IBlogService.CreateBlog(DataModels.Blog blog)
        {
            var Blog = this.Repository.GetBlogs().FirstOrDefault(b => b.Id == blog.Id);

            if (Blog == null)
            {
                Blog = this.Repository.CreateBlog();
            }
            else
            {
                this.Repository.Attach(Blog);
            }

            Blog.BloggerId = blog.BloggerId;
            Blog.BlogName = blog.BlogName;

            this.Repository.Save();
        }

        void IBlogService.UpdateBlogDetails(DataModels.Blog blog)
        {
            (this as IBlogService).CreateBlog(blog);
        }

        void IBlogService.DeleteBlog(int blogId)
        {
            var Blog = this.Repository.GetBlogs().FirstOrDefault(b => b.Id == blogId);

            if (Blog != null)
            {
                this.Repository.Delete(Blog);
                this.Repository.Save();
            }
        }

        IEnumerable<DataModels.Entry> IBlogService.GetBlogEntries(int blogId)
        {
            List<DataModels.Entry> Result = new List<DataModels.Entry>();

            var Blog = this.Repository.GetBlogs().FirstOrDefault(b => b.Id == blogId);

            if (Blog != null)
            {
                Result = this.Repository.GetEntries().Where(e => e.BlogId == blogId && e.Published).ToList();
            }

            return Result;
        }

        IEnumerable<DataModels.Entry> IBlogService.GetNonPublishedEntries(int blogId)
        {
            List<DataModels.Entry> Result = new List<DataModels.Entry>();

            var Blog = this.Repository.GetBlogs().FirstOrDefault(b => b.Id == blogId);

            if (Blog != null)
            {
                Result = this.Repository.GetEntries().Where(e => e.BlogId == blogId && !e.Published).ToList();
            }

            return Result;
        }

        IEnumerable<DataModels.Entry> IBlogService.GetBlogEntries(int blogId, int page, int entriesPerPage)
        {
            List<DataModels.Entry> Result = new List<DataModels.Entry>();

            if (page > 0 && entriesPerPage > 0)
            {
                Result = this.Repository.GetEntries().Where(b => b.BlogId == blogId && b.Published)
                    .OrderBy(b => b.CreateDate)
                    .ThenBy(b => b.EditedDate)
                    .Skip(page * entriesPerPage - entriesPerPage)
                    .ToList();
            }

            return Result;
        }
    }
}
