using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogEngineMvc.ViewModels
{
    public class CreateEntryViewModel
    {
        public bool IsRawHtml { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string EntryText { get; set; }
        public string Tags { get; set; }
    }
}