using Gamification.Shared.Core.Domain;
using Gamification.Shared.Core.Entities;

namespace Gamification.Shared.Core.Features
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
