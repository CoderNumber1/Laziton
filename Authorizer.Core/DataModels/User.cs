using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using Authorizer.Security.Session;

namespace Authorizer.DataModels
{
    [Table("Users")]
    [DataContract]
    public class User
    {
        [Key]
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DataMember]
        [Column(TypeName = "varchar")]
        [MaxLength(100)]
        public string OpenId { get; set; }

        [DataMember]
        [Column(TypeName = "varchar")]
        [MaxLength(150)]
        public string EmailFull { get; set; }

        [DataMember]
        [Column(TypeName = "varchar")]
        [MaxLength(100)]
        public string EmailUserName { get; set; }

        [ForeignKey("Alias")]
        public int? AliasId { get; set; }
        public virtual UserAlias Alias { get; set; }
    }

    [Table("Alias")]
    [DataContract]
    public class UserAlias
    {
        [Key]
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataMember]
        [Column(TypeName = "varchar")]
        [MaxLength(20)]
        [Required]
        [Display(Name = "Alias Name")]
        public string Name { get; set; } //This needs to be created with a unique key in a database initializer

        [DataMember]
        public int PermissionLevel { get; set; }

        public List<User> Users { get; set; }
    }
}
