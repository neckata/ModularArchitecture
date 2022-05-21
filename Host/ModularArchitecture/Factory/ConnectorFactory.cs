using Slack.Core.Services;
using ModularArchitecture.Shared.Core.Enums;
using ModularArchitecture.Shared.Core.Interfaces.Services.Connector;
using ModularArchitecture.Shared.Core;
using Outlook.Core.Services;
using System;
using Slack.Core.Interfaces;

namespace Host.ModularArchitecture.Factory
{
    public class ConnectorFactory : IConnectorFactory
    {
        readonly IServiceProvider _container;

        public ConnectorFactory(IServiceProvider container)
        {
            _container = container;
        }

        public IConnectorClient CreateFor(ConnectorTypeEnum connectorType)
        {
            switch (connectorType)
            {
                case ConnectorTypeEnum.Slack:
                    return (SlackConnectorClient)_container.GetService(typeof(ISlackClient));
                case ConnectorTypeEnum.Outlook:
                    return (OutlookConnectorClient)_container.GetService(typeof(IOutlookClient));
            }

            throw new ArgumentException($"Unknown connectorType '{connectorType}'");
        }
    }
}
