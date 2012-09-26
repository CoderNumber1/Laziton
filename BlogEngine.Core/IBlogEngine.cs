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

        bool IsBloggerRegistered(int userId);
        void RegisterBlogger(Blogger blogger);
        BloggerPModel GetBloggerInfo(int userId);

        IEnumerable<Entry> GetBlogEntries(int blogId);
        Entry GetBlogEntry(int entryId);
        void AddEntry(Entry blogEntry);
        void UpdateEntry(Entry blogEntry);
        void DeleteEntry(int entryId);

        IEnumerable<Comment> GetComments(int entryId);
        void LeaveComment(Comment comment);
        void RespondToComment(Comment response);
    }
}
