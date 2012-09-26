using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Authorizer.DataModels;

namespace Authorizer.DataContexts
{
    public class AuthorizerContext
        : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserAlias> Aliases { get; set; }

        public AuthorizerContext(string connectionString)
            : base(connectionString) 
        { 
            //this.Configuration.LazyLoadingEnabled = false;
        }
    }

    public class AuthorizerInitializer : DropCreateDatabaseAlways<AuthorizerContext>
    {
        protected override void Seed(AuthorizerContext context)
        {
            base.Seed(context);

            context.Database.ExecuteSqlCommand("CREATE UNIQUE INDEX IX_User_Alias ON Alias (Name)");
        }
    }
}
