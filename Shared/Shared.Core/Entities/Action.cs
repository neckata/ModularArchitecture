using ModularArchitecture.Shared.Core.Domain;
using ModularArchitecture.Shared.Core.Enums;

namespace ModularArchitecture.Shared.Core.Entities
{
    public class Action : BaseEntity
    {
        public ConnectorTypeEnum ConnectorType { get; set; }

        public Action()
        {

        }
    }
}
