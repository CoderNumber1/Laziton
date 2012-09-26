using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using CF = ConfigurationFramework;

namespace KarlAnthonyJames.Com.Core.Configuration
{
    public class CoreConfiguration
    {
        public CF.Config Config;

        private CoreConfiguration() 
        {
            this.Config = new CF.Config(System.Configuration.ConfigurationManager.AppSettings["KAJC_Core_ConfigName"], null, CF.Configuration.SourceMode.File, CF.Configuration.SourceLocation.Central, false, false);
        }
        private static Lazy<CoreConfiguration> ConfigInstance = new Lazy<CoreConfiguration>(() => new CoreConfiguration());
        public static CoreConfiguration Instance { get { return ConfigInstance.Value; } }

        public int BlogId
        {
            get { return this.Config.GetInt("Blog", "BlogId"); }
        }

        public string NavPath
        {
            get { return this.Config.GetString("Pages", "NavPath"); }
        }

        public string IgnorableControllers
        {
            get { return this.Config.GetString("Routing", "IgnorableControllers"); }
        }

        public string IgnorableActions
        {
            get { return this.Config.GetString("Routing", "IgnorableActions"); }
        }

        public string AuthorizerConnectionString
        {
            get { return this.Config.GetString("SQLConnections", "Authorizer"); }
        }
    }
}
