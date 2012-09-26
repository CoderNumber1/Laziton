using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authorizer.Configuration;
using Authorizer.DataRepositories;

namespace Authorizer.DataServices
{
    public class AuthorizerDataService
        : IAuthorizerDataService
    {
        private IAuthorizerDataService ThisService { get { return this as IAuthorizerDataService; } }

        public AuthorizerDataService(IAuthorizerConfiguration config)
        {
            this._Repository = new AuthorizerRepository(config);
        }

        private DataRepositories.IAuthorizerRepository _Repository;
        DataRepositories.IAuthorizerRepository IAuthorizerDataService.Repository
        {
            get { return this._Repository; }
        }

        void IAuthorizerDataService.RegisterUserLogIn(DataModels.User user)
        {
            var DbUser = ThisService.Repository.GetUsers().FirstOrDefault(u => u.OpenId == user.OpenId);

            if (DbUser == null)
            {
                ThisService.Repository.AddUser(user);
                ThisService.Repository.Save();
            }
        }

        DataModels.User IAuthorizerDataService.RetrieveUserInfo(string openId)
        {
            return ThisService.Repository.GetUsers().FirstOrDefault(u => u.OpenId == openId);
        }

        void IAuthorizerDataService.RegisterUserAlias(string openId, DataModels.UserAlias alias)
        {
            var DbUser = ThisService.Repository.GetUsers().FirstOrDefault(u => u.OpenId == openId);
            var DbAlias = ThisService.Repository.GetAliases().FirstOrDefault(a => a.Id == DbUser.AliasId);

            if (DbAlias == null)
            {
                ThisService.Repository.AddAlias(alias);
                ThisService.Repository.Save();
                DbUser.AliasId = ThisService.Repository.Context.Entry(alias).Entity.Id;
                ThisService.Repository.UpdateUser(DbUser);
                ThisService.Repository.Save();
            }
        }

        DataModels.UserAlias IAuthorizerDataService.RetrieveUserAlias(string openId)
        {
            return ThisService.Repository.GetUsers().Where(u => u.OpenId == openId)
                    .Join(ThisService.Repository.GetAliases()
                        , u => u.AliasId
                        , a => a.Id
                        , (u, a) => a).FirstOrDefault();
        }

        #region IDisposable Implementation
        private bool Disposed = false;
        void IDisposable.Dispose()
        {
            this.Dispose(true);
        }

        ~AuthorizerDataService()
        {
            this.Dispose(false);
        }

        private void Dispose(bool dispose)
        {
            if (!this.Disposed)
            {
                if (dispose)
                {
                    ThisService.Repository.Dispose();
                    this.Disposed = true;
                }
            }
        }
        #endregion
    }
}
