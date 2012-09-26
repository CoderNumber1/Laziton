using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authorizer.DataModels;

namespace Authorizer.DataRepositories
{
    public interface IAuthorizerRepository
        : IDisposable
    {
        DataContexts.AuthorizerContext Context { get; }

        IQueryable<DataModels.User> GetUsers();
        IQueryable<DataModels.UserAlias> GetAliases();

        void AddUser(DataModels.User user);
        void UpdateUser(DataModels.User user);
        void RemoveUser(DataModels.User user);
        void RemoveUser(int userId);

        void AddAlias(DataModels.UserAlias alias);
        void UpdateAlias(DataModels.UserAlias alias);
        void RemoveAlias(DataModels.UserAlias alias);
        void RemoveAlias(int aliasId);

        void Save();
    }
}
