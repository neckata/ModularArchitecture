﻿using Gamification.Shared.Core.Domain;
using Gamification.Shared.Core.Entities;

namespace Gamification.Shared.Core.Features
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
