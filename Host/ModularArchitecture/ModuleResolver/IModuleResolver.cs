using ModularArchitecture.Shared.Infrastructure.Enums;

namespace Host.ModularArchitecture.ModuleResolver
{
    /// <summary>
    /// Provides access to Module to be used
    /// </summary>
    public interface IModuleResolver
    {
        /// <summary>
        /// Creates a action command for MediatR to be used agaisnt specific Module
        /// </summary>
        /// <param name="moduleType"></param>
        /// <param name="request"></param>
        /// <param name="actionsType"></param>
        /// <returns>Command object</returns>
        public object CreateCommand(string moduleType, object request, ActionsTypeEnum actionsType);
    }
}
