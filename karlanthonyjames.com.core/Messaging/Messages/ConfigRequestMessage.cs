using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KarlAnthonyJames.Com.Core.Messaging.Results;

namespace KarlAnthonyJames.Com.Core.Messaging.Messages
{
    public class ConfigRequestMessage
        : MvcContrib.PortableAreas.ICommandMessage<ConfigRequestResult>
    {
        private ConfigRequestResult _Result;
        public ConfigRequestResult Result { get { if (_Result == null) _Result = new ConfigRequestResult(); return _Result; } }
    }
}
