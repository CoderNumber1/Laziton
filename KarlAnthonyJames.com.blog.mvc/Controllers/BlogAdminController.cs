﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogEngineMvc.ViewModels;
using BlogEngine.Core;
using BlogEngine.Core.DataModels;
using KarlAnthonyJames.Com.Core.Configuration;
using MvcWebDev.Auth.Security.Attributes;
//using Authorizer.Security.Attributes;
//using Authorizer.Security.Session;

namespace BlogEngineMvc.Controllers
{
    [Authorize(Roles="BlogAdmin")]
    public class BlogAdminController : JSONGetsController
    {
        private IBlogEngine BlogEngine { get { return SQLBlogEngine.Engine; } }

        public ActionResult RedirectHome()
        {
            return this.RedirectToAction("Index", "Blog", new { area = "Blog" });
        }

        public ActionResult Drafts()
        {
            return View("~/Areas/Blog/Views/Blog/Index.cshtml");
        }

        public override JsonResult GetJSONEntries()
        {
            var Entries = BlogEngine.GetNonPublishedEntries(CoreConfiguration.Instance.BlogId).ToList();
            return new JsonResult() { Data = Entries, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult CreateEntry()
        {
            return View(new CreateEntryViewModel());
        }

        [HttpPost]
        public ActionResult CreateEntry(CreateEntryViewModel entryViewModel)
        {
            MarkupSanitizer sanitizer = new MarkupSanitizer();
            entryViewModel.EntryText = sanitizer.Sanitize(string.IsNullOrEmpty(entryViewModel.EntryText) ? string.Empty : entryViewModel.EntryText);
            entryViewModel.Tags = sanitizer.Sanitize(string.IsNullOrEmpty(entryViewModel.Tags) ? string.Empty : entryViewModel.Tags);
            entryViewModel.Title = sanitizer.Sanitize(string.IsNullOrEmpty(entryViewModel.Title) ? string.Empty : entryViewModel.Title);
            BlogEngine.AddEntry(new BlogEngine.Core.DataModels.Entry()
            {
                BlogId = CoreConfiguration.Instance.BlogId,
                EntryText = entryViewModel.EntryText,
                IsRawHtml = true,
                Published = entryViewModel.Published,
                Tags = entryViewModel.Tags,
                Title = entryViewModel.Title,
                CreateDate = DateTime.Now
            });

            return this.RedirectHome();
        }

        public ActionResult EditEntry(int id)
        {
            return View(BlogEngine.GetBlogEntry(id));
        }

        [HttpPost]
        public ActionResult EditEntry(Entry entry)
        {
            MarkupSanitizer sanitizer = new MarkupSanitizer();

            var OriginalEntry = BlogEngine.GetBlogEntry(entry.Id);
            OriginalEntry.Title = sanitizer.Sanitize(entry.Title);
            OriginalEntry.EntryText = sanitizer.Sanitize(entry.EntryText);
            OriginalEntry.Published = entry.Published;
            OriginalEntry.IsRawHtml = true; //we need to consider getting rid of this, probably not needed anymore.
            OriginalEntry.Tags = sanitizer.Sanitize(entry.Tags);
            OriginalEntry.EditedDate = DateTime.Now;
            BlogEngine.UpdateEntry(OriginalEntry);
            return this.RedirectHome();
        }

        public ActionResult DeleteEntry(int id)
        {
            Session["EntryToDelete"] = id;
            return View(BlogEngine.GetBlogEntry(id));
        }

        public ActionResult ConfirmDeleteEntry(int id)
        {
            if ((int)Session["EntryToDelete"] == id)
            {
                Session.Remove("EntryToDelete");
                this.BlogEngine.DeleteEntry(id);
            }

            return this.RedirectHome();
        }
    }
}
