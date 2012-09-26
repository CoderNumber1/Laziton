using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlogEngine.Core.DataRepository;
using BlogEngine.Core.DataModels;
using BlogEngine.Core.Configuration;

namespace BlogEngine.Core.DataServices
{
    public interface IBlogService
        : IDisposable
    {
        IBlogRepository Repository { get; }

        bool IsBloggerRegistered(int UserId);
        void RegisterBlogger(Blogger blogger);
        Blogger GetBlogger(int userId);

        IEnumerable<Blog> GetBlogs(int bloggerId);
        void CreateBlog(Blog blog);
        void UpdateBlogDetails(Blog blog);
        void DeleteBlog(int blogId);

        IEnumerable<Entry> GetBlogEntries(int blogId);
        IEnumerable<Entry> GetBlogEntries(int blogId, DateTime entryDate, bool? assending = true);
        IEnumerable<Entry> GetBlogEntries(int blogId, int page, int entriesPerPage);

        IEnumerable<Comment> GetBlogComments(int entryId);
        IEnumerable<Comment> GetBlogCommentResponses(int commentId);

        void AddBlogEntry(Entry entry);
        void UpdateBlogEntry(Entry entry);
        void RemoveBlogEntry(Entry entry);
        void RemoveBlogEntry(int entryID);

        void AddComment(Comment comment);
        void RemoveComment(Comment comment);
        void RemoveComment(int commentID);
    }
}
