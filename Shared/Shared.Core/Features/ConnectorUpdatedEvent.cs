using Gamification.Shared.Core.Domain;
using Gamification.Shared.Core.Entities;

namespace Gamification.Shared.Core.Features
{
    public class ConnectorUpdatedEvent : Event
    {
        public string Id { get; }

        public ConnectorUpdatedEvent(Connector connector)
        {
            Id = connector.Id;
        }
    }
}
