using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContentManagement.Models.Mappings
{
    public class ContentModeMap : EntityTypeConfiguration<ContentMode>
    {
        public ContentModeMap()
        {
            this.Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("Id");

            this.Property(p => p.Name)
                .HasColumnName("Name");
        }
    }
}
