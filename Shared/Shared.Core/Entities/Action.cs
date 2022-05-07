using Gamification.Shared.Core.Domain;
using Gamification.Shared.Core.Enums;

namespace Gamification.Shared.Core.Entities
{
    public class Action : BaseEntity
    {
        public ConnectorTypeEnum ConnectorType { get; set; }

        public Action()
        {

        }
    }
}
