using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authorizer.Configuration
{
    public class AuthServiceConfiguration
    {
        internal Uri HomeUri { get; set; }

        public AuthServiceConfiguration(Uri homeUri)
        {
            this.HomeUri = homeUri;
        }
    }
}
