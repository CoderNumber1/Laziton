using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KarlAnthonyJames.Com.Core.Messaging.Messages;
using MvcContrib.PortableAreas;
using KarlAnthonyJames.Com.Core.Messaging.Results;
using KarlAnthonyJames.Com.Core.Configuration;

namespace KarlAnthonyJames.Com.Core.Messaging.Handlers
{
    public class ConfigRequestHandler
        : MvcContrib.PortableAreas.MessageHandler<ConfigRequestMessage>
    {
        public override void Handle(ConfigRequestMessage message)
        {
            var configRequestMessage = (message as ICommandMessage<ConfigRequestResult>);
            configRequestMessage.Result.Config = Configuration.CoreConfiguration.Instance.Config;
            configRequestMessage.Result.Success = true;
        }
    }
}
