using ModularArchitecture.Shared.Infrastructure.Enums;

namespace Host.ModularArchitecture.Factory
{
    public interface IConnectorFactory
    {
        public object CreateCommand(string connectorType, object request, ActionsTypeEnum actionsType);
    }
}
