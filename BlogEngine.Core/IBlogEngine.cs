using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlogEngine.Core.DataModels;
using BlogEngine.Core.DataContexts;
using KarlAnthonyJames.Com.Core.Configuration;
using BlogEngine.Core.Configuration;
using BlogEngine.Core.PresentationModels;

namespace BlogEngine.Core
{
    public interface IBlogEngine
    {
        IBlogEngineConfiguration Config { get; }

        void StartEngine(ConfigurationFramework.Config config);
        void StartEngine(string configName);

        bool IsBloggerRegistered(string userName);
        void RegisterBlogger(Blogger blogger);
        BloggerPModel GetBloggerInfo(string userName);

        IEnumerable<Blog> GetBlogs(int bloggerId);
        Blog GetBlog(int blogId);
        Blog GetBlog(string blogName, int bloggerId);
        void CreateBlog(Blog blog);
        void DeleteBlog(int blogId);

        IEnumerable<Entry> GetBlogEntries(int blogId);
        IEnumerable<Entry> GetBlogEntriesByTag(int blogId, string tag);
        Entry GetBlogEntry(int entryId);
        Entry GetBlogEntry(string title, int blogId);
        void AddEntry(Entry blogEntry);
        void UpdateEntry(Entry blogEntry);
        void DeleteEntry(int entryId);

        IEnumerable<Entry> GetNonPublishedEntries(int blogId);

        IEnumerable<Comment> GetComments(int entryId);
        void LeaveComment(Comment comment);
        void RespondToComment(Comment response);
    }
}
