using BlogEngine.Core;
using BlogEngine.Core.DataModels;
using BlogEngine.Core.PresentationModels;
using KarlAnthonyJames.Com.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.ViewModels
{
    public enum ContentSource { Content, Blog }
    public enum ContentMode { Latest, Selected }

    public class PageContentViewModel
    {
        IBlogEngine ContentEngine { get { return BlogEngine.Core.SQLBlogEngine.Engine; } }

        public ContentSource MainContentSource { get; set; }
        public ContentMode ContentMode { get; set; }

        public Entry MainContent { get; set; }

        public PageContentViewModel()
        {
            BloggerPModel ContentUserInfo;

            if(!ContentEngine.IsBloggerRegistered(CoreConfiguration.Instance.ContentUser))
            {
                Blogger ContentUser = new Blogger();
                ContentUser.UserName = CoreConfiguration.Instance.ContentUser;

                ContentEngine.RegisterBlogger(ContentUser);
            }

            ContentUserInfo = ContentEngine.GetBloggerInfo(CoreConfiguration.Instance.ContentUser);

            if (!ContentUserInfo.Blogs.Any(blog => blog.Id == CoreConfiguration.Instance.ContentSourceId))
            {
                Blog ContentBlog = new Blog();
                ContentBlog.BloggerId = ContentUserInfo.BloggerInfo.Id;
                ContentBlog.BlogName = "Site Content";

                ContentEngine.CreateBlog(ContentBlog);
            }


        }
    }
}