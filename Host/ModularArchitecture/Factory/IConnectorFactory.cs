using ModularArchitecture.Shared.Infrastructure.Enums;

namespace Host.ModularArchitecture.Factory
{
    /// <summary>
    /// Provides access to connector to be used
    /// </summary>
    public interface IConnectorFactory
    {
        /// <summary>
        /// Creates a action command for MediatR to be used agaisnt specific connector
        /// </summary>
        /// <param name="connectorType"></param>
        /// <param name="request"></param>
        /// <param name="actionsType"></param>
        /// <returns>Command object</returns>
        public object CreateCommand(string connectorType, object request, ActionsTypeEnum actionsType);
    }
}
