using ModularArchitecture.Shared.Core.Domain;
using ModularArchitecture.Shared.Core.Entities;

namespace ModularArchitecture.Shared.Core.Features
{
    public class ActionUpdatedEvent : Event
    {
        public System.Guid Id { get; }

        public ActionUpdatedEvent(Action action)
        {
            Id = action.Id;
            RelatedEntities = new[] { typeof(Action) };
        }
    }
}
