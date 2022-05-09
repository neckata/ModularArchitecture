using ModularArchitecture.Shared.Core.Enums;
using ModularArchitecture.Shared.Core.Interfaces.Services.Connector;

namespace ModularArchitecture.Shared.Core
{
    public interface IConnectorFactory
    {
        public IConnectorClient CreateFor(ConnectorTypeEnum connectorType);
    }
}
