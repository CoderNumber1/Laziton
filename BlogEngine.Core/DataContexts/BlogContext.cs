using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using BlogEngine.Core.DataModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogEngine.Core.DataContexts
{
    public class BlogContext
        : DbContext
    {
        public DbSet<Blogger> Bloggers { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public BlogContext(string ContextConnectionString)
            : base(ContextConnectionString)
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Blogger Table Setup
            modelBuilder.Entity<Blogger>().ToTable("Blogger");
            modelBuilder.Entity<Blogger>().Property(blogger => blogger.Id).HasColumnName("Blogger_ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Blogger>().Property(blogger => blogger.UserId).HasColumnName("User_ID");
            modelBuilder.Entity<Blogger>().Property(blogger => blogger.DisplayName).HasColumnName("Display_Name");
            modelBuilder.Entity<Blogger>().Property(blogger => blogger.Signature).HasColumnName("Blogger_Signature");
            #endregion

            #region Blog 
            modelBuilder.Entity<Blog>().ToTable("Blogs");
            modelBuilder.Entity<Blog>().Property(blog => blog.Id).HasColumnName("Blog_ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Blog>().Property(blog => blog.BlogName).HasColumnName("Blog_Name");

            modelBuilder.Entity<Blog>().HasRequired(blog => blog.Blogger).WithMany().HasForeignKey(blog => blog.BloggerId);
            modelBuilder.Entity<Blog>().Property(blog => blog.BloggerId).HasColumnName("Blogger_ID");
            #endregion

            #region Entry Table Setup
            modelBuilder.Entity<Entry>().ToTable("Blog_Entries");
            modelBuilder.Entity<Entry>().Property(entry => entry.Id)
                .HasColumnName("Entry_ID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Entry>().Property(entry => entry.IsRawHtml)
                .HasColumnName("Entry_Is_Raw_Html");
            modelBuilder.Entity<Entry>().Property(entry => entry.Title)
                .HasColumnName("Entry_Title");
            modelBuilder.Entity<Entry>().Property(entry => entry.Tags)
                .HasColumnName("Entry_Tags")
                .IsOptional();
            modelBuilder.Entity<Entry>().Property(entry => entry.EntryText)
                .HasColumnName("Entry_Content");
            modelBuilder.Entity<Entry>().Property(entry => entry.CreateDate)
                .HasColumnName("Entry_Create_Date");
            modelBuilder.Entity<Entry>().Property(entry => entry.EditedDate)
                .HasColumnName("Entry_Edit_Date");

            modelBuilder.Entity<Entry>().HasRequired(entry => entry.Blog).WithMany().HasForeignKey(entry => entry.BlogId);
            modelBuilder.Entity<Entry>().Property(entry => entry.BlogId).HasColumnName("Blog_Id");
            #endregion
            
            #region Comments Table Setup
            modelBuilder.Entity<Comment>().ToTable("Blog_Comments");
            modelBuilder.Entity<Comment>().Property(comment => comment.Id)
                .HasColumnName("Comment_ID")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Comment>().Property(comment => comment.By)
                .HasColumnName("Comment_Author")
                .IsRequired();
            modelBuilder.Entity<Comment>().Property(comment => comment.Content)
                .HasColumnName("Comment_Content")
                .IsRequired();
            modelBuilder.Entity<Comment>().Property(comment => comment.CommentDate)
                .HasColumnName("Comment_Date");
            modelBuilder.Entity<Comment>().Property(comment => comment.CanRespond)
                .HasColumnName("Comment_Respondable");
            modelBuilder.Entity<Comment>().HasRequired(comment => comment.BlogEntry)
                .WithMany()
                .HasForeignKey(comment => comment.EntryId);
            modelBuilder.Entity<Comment>().Property(comment => comment.EntryId)
                .HasColumnName("Comment_Entry_ID");
            modelBuilder.Entity<Comment>().HasOptional(comment => comment.ResponseComment)
                .WithMany()
                .HasForeignKey(comment => comment.ResponseId);
            modelBuilder.Entity<Comment>().Property(comment => comment.ResponseId)
                .HasColumnName("Comment_Responseto_ID");
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
