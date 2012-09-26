using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KarlAnthonyJames.Com.Core.Messaging.Messages;
using Authorizer.Configuration;

namespace Authorizer.DataServices
{
    public class DataServiceProvider
    {
        private static Lazy<DataServiceProvider> _Instance = new Lazy<DataServiceProvider>(() => new DataServiceProvider());
        public static DataServiceProvider Instance { get { return _Instance.Value; } }
        private DataServiceProvider()
        {
            var ConfigRequest = new ConfigRequestMessage();
            MvcContrib.Bus.Send(ConfigRequest);

            if (ConfigRequest.Result.Success)
                this.Config = new AuthorizerConfiguration(ConfigRequest.Result.Config);
            else
                this.Config = new AuthorizerConfiguration(new ConfigurationFramework.Config(System.Configuration.ConfigurationManager.AppSettings["Authorizer_ConfigName"]));
        }

        private IAuthorizerConfiguration Config;

        public IAuthorizerDataService CreateAuthorizerDataService()
        {
            if (this.Config != null)
                return new AuthorizerDataService(this.Config);
            else
                return null;
        }
    }
}
