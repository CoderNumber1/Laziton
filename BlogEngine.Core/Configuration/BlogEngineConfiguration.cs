using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CF = ConfigurationFramework;

namespace BlogEngine.Core.Configuration
{
    public class BlogEngineConfiguration : IBlogEngineConfiguration
    {
        private CF.Config Configuration;

        public BlogEngineConfiguration(string configName)
        {
            this.Configuration = new CF.Config(configName);
        }

        public BlogEngineConfiguration(CF.Config config)
        {
            this.Configuration = config;
        }

        string IBlogEngineConfiguration.ContextConnectionString
        {
            get { return this.Configuration.GetString("SQLConnections", "BlogString"); }
        }
    }
}
