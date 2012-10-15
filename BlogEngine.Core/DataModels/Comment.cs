using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogEngine.Core.DataModels
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string By { get; set; }
        public string ByUserId { get; set; }
        public string ByEmail { get; set; }
        public bool CanRespond { get; set; }
        public DateTime CommentDate { get; set; }

        [NotMapped]
        public string CommentDateAsString
        {
            get { return this.CommentDate.ToString("MMMM d yyyy h:mm:ss tt"); }
        }

        public int EntryId { get; set; }
        public Entry BlogEntry { get; set; }

        public int? ResponseId { get; set; }
        public Comment ResponseComment { get; set; }
    }
}
