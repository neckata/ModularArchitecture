using System;
using System.Linq;
using System.Reflection;
using ModularArchitecture.DTOs.Actions;
using ModularArchitecture.Shared.Infrastructure.Enums;

namespace Host.ModularArchitecture.Factory
{
    public class ConnectorFactory : IConnectorFactory
    {
        public object CreateCommand(string connectorType, object request, ActionsTypeEnum actionsType)
        {
            Assembly module = AppDomain.CurrentDomain.GetAssemblies().First(x => x.FullName.Contains(connectorType));
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

            throw new ArgumentException($"Unknown connectorType '{connectorType}'");
        }
    }
}
