using Gamification.Shared.Core.Enums;
using Gamification.Shared.Core.Interfaces.Services.Connector;

namespace Gamification.Shared.Core
{
    public interface IConnectorFactory
    {
        public IConnectorClient CreateFor(ConnectorTypeEnum connectorType);
    }
}
