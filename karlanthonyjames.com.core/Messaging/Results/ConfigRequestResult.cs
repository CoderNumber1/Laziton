using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KarlAnthonyJames.Com.Core.Messaging.Results
{
    public class ConfigRequestResult
        : MvcContrib.PortableAreas.ICommandResult
    {
        public bool Success { get; set; }
        public ConfigurationFramework.Config Config { get; set; }
    }
}
