using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogEngineMvc.ViewModels
{
    public class EditEntryViewModel
    {
        public string EntryText { get; set; }
        public bool IsRawHtml { get; set; }
        public string Tags { get; set; }
        public string Title { get; set; }

    }
}