using BlogEngine.Core.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogEngineMvc.ViewModels
{
    public class CreateEntryViewModel
    {
        public CreateEntryViewModel() { }
        public CreateEntryViewModel(Entry entry)
        {
            this.IsRawHtml = entry.IsRawHtml;
            this.Title = entry.Title;
            this.EntryText = entry.EntryText;
            this.Tags = entry.Tags;

            this.Published = entry.Published;
        }

        public bool Published { get; set; }

        public bool IsRawHtml { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string EntryText { get; set; }
        public string Tags { get; set; }
    }
}