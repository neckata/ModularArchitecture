using ModularArchitecture.DTOs.Actions;
using ModularArchitecture.Shared.Infrastructure.Enums;
using System;
using System.Linq;
using System.Reflection;

namespace Host.ModularArchitecture.ModuleResolver
{
    /// <summary>
    /// Provides access to Module to be used
    /// </summary>
    public class ModuleResolver : IModuleResolver
    {
        /// <summary>
        /// Creates a action command for MediatR to be used agaisnt specific Module
        /// </summary>
        /// <param name="moduleType">Module</param>
        /// <param name="request">Data</param>
        /// <param name="actionsType">Type of action</param>
        /// <returns>Command object</returns>
        public object CreateCommand(string moduleType, object request, ActionsTypeEnum actionsType)
        {
            Assembly module = AppDomain.CurrentDomain.GetAssemblies().First(x => x.FullName.Contains(moduleType));
            switch (actionsType)
            {
                case ActionsTypeEnum.Create:
                    {
                        Type createActionCommand = module.GetTypes().First(x => x.Name == "CreateActionCommand");
                        ConstructorInfo createActionCommandConstructor = createActionCommand.GetConstructor(new[] { typeof(CreateActionRequest) });
                        object createActionCommandConstructorInstance = createActionCommandConstructor.Invoke(new object[] { request });
                        return createActionCommandConstructorInstance;
                    }
                case ActionsTypeEnum.Update:
                    {
                        Type updateActionCommand = module.GetTypes().First(x => x.Name == "UpdateActionCommand");
                        ConstructorInfo updateActionCommandConstructor = updateActionCommand.GetConstructor(new[] { typeof(UpdateActionRequest) });
                        object updateActionCommandConstructorInstance = updateActionCommandConstructor.Invoke(new object[] { request });
                        return updateActionCommandConstructorInstance;
                    }
                case ActionsTypeEnum.View:
                    {
                        Type getActionsCommand = module.GetTypes().First(x => x.Name == "GetActionsCommand");
                        ConstructorInfo getActionsCommandConstructor = getActionsCommand.GetConstructor(Type.EmptyTypes);
                        object getActionsCommandConstructorInstance = getActionsCommandConstructor.Invoke(new object[] { });
                        return getActionsCommandConstructorInstance;
                    }
            }

            throw new ArgumentException($"Unknown ModuleType '{moduleType}'");
        }
    }
}
