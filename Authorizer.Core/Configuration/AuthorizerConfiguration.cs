using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CF = ConfigurationFramework;

namespace Authorizer.Configuration
{
    public class AuthorizerConfiguration : IAuthorizerConfiguration
    {
        private CF.Config Config;

        public AuthorizerConfiguration(string configName)
        {
            this.Config = new CF.Config(configName);
        }

        public AuthorizerConfiguration(CF.Config config)
        {
            this.Config = config;
        }

        string IAuthorizerConfiguration.AuthorizerConnectionString
        {
            get { return this.Config.GetString("SQLConnections", "Authorizer"); }
        }
    }
}
