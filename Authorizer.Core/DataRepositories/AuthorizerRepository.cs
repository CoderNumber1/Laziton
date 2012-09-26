using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authorizer.Configuration;
using Authorizer.DataContexts;

namespace Authorizer.DataRepositories
{
    public class AuthorizerRepository
        : IAuthorizerRepository
    {
        public AuthorizerRepository(IAuthorizerConfiguration config)
        {
            this._Context = new AuthorizerContext(config.AuthorizerConnectionString);
        }

        private IAuthorizerRepository ThisRep { get { return (this as IAuthorizerRepository); } }

        private AuthorizerContext _Context;
        AuthorizerContext IAuthorizerRepository.Context
        {
            get { return this._Context; }
        }

        IQueryable<DataModels.User> IAuthorizerRepository.GetUsers()
        {
            return ThisRep.Context.Users;        
        }

        IQueryable<DataModels.UserAlias> IAuthorizerRepository.GetAliases()
        {
            return ThisRep.Context.Aliases;
        }

        void IAuthorizerRepository.AddUser(DataModels.User user)
        {
            ThisRep.Context.Entry(user).State = System.Data.EntityState.Added;
        }

        void IAuthorizerRepository.UpdateUser(DataModels.User user)
        {
            ThisRep.Context.Entry(user).State = System.Data.EntityState.Modified;
        }

        void IAuthorizerRepository.RemoveUser(DataModels.User user)
        {
            ThisRep.Context.Entry(user).State = System.Data.EntityState.Deleted;
        }

        void IAuthorizerRepository.RemoveUser(int userId)
        {
            var DbUser = ThisRep.Context.Users.First(User => User.Id == userId);
            ThisRep.Context.Entry(DbUser).State = System.Data.EntityState.Deleted;
        }

        void IAuthorizerRepository.AddAlias(DataModels.UserAlias alias)
        {
            ThisRep.Context.Entry(alias).State = System.Data.EntityState.Added;
        }

        void IAuthorizerRepository.UpdateAlias(DataModels.UserAlias alias)
        {
            ThisRep.Context.Entry(alias).State = System.Data.EntityState.Modified;
        }

        void IAuthorizerRepository.RemoveAlias(DataModels.UserAlias alias)
        {
            ThisRep.Context.Entry(alias).State = System.Data.EntityState.Deleted;
        }

        void IAuthorizerRepository.RemoveAlias(int aliasId)
        {
            var DbAlias = ThisRep.Context.Aliases.First(Alias => Alias.Id == aliasId);
            ThisRep.Context.Entry(DbAlias).State = System.Data.EntityState.Deleted;
        }

        void IAuthorizerRepository.Save()
        {
            ThisRep.Context.SaveChanges();
        }

        #region IDisposable Implementation
        private bool Disposed = false;

        void IDisposable.Dispose()
        {
            this.Dispose(true);
        }

        ~AuthorizerRepository()
        {
            this.Dispose(false);
        }

        private void Dispose(bool dispose)
        {
            if (dispose)
            {
                if (!this.Disposed)
                {
                    ThisRep.Context.Dispose();
                    this.Disposed = true;
                }
            }
        }
        #endregion
    }
}
