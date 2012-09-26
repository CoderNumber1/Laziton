using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Core.DataModels
{
    public class Entry
    {
        public int Id { get; set; }
        public bool IsRawHtml { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string EntryText { get; set; }
        public string Tags { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? EditedDate { get; set; }

        [NotMapped]
        public string CreateDateAsString
        {
            get { return this.CreateDate.ToString("MMMM d yyyy h:mm:ss tt"); }
        }

        [NotMapped]
        public string EditedDateAsString
        {
            get { return this.EditedDate != null ? this.EditedDate.Value.ToString() : string.Empty; }
        }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        //public virtual List<Comment> Comments { get; set; }
    }
}
