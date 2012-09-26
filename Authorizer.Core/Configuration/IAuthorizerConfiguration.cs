using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authorizer.Configuration
{
    public interface IAuthorizerConfiguration
    {
        string AuthorizerConnectionString { get; }
    }
}
