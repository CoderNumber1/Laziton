using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authorizer.DataRepositories;

namespace Authorizer.DataServices
{
    public interface IAuthorizerDataService
        : IDisposable
    {
        IAuthorizerRepository Repository { get; }

        void RegisterUserLogIn(DataModels.User user);
        DataModels.User RetrieveUserInfo(string openId);

        void RegisterUserAlias(string openId, DataModels.UserAlias alias);
        DataModels.UserAlias RetrieveUserAlias(string openId);
    }
}
