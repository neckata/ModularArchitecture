using ModularArchitecture.Shared.Core.Domain;
using ModularArchitecture.Shared.Core.Entities;

namespace ModularArchitecture.Shared.Core.Features
{
    public class ActionAddEvent : Event
    {
        public System.Guid Id { get; }

        public ActionAddEvent(Action action)
        {
            Id = action.Id;
            RelatedEntities = new[] { typeof(Action) };
        }
    }
}
