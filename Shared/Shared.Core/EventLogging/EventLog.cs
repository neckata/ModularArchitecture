using System;
using ModularArchitecture.Shared.Core.Contracts;
using ModularArchitecture.Shared.Core.Domain;

namespace ModularArchitecture.Shared.Core.EventLogging
{
    public class EventLog : Event, IEntity<Guid>
    {
        public EventLog(Event theEvent, string data, (string oldValues, string newValues) changes, Guid userId)
        {
            Id = Guid.NewGuid();
            AggregateId = theEvent.AggregateId;
            MessageType = theEvent.MessageType;
            Data = data;
            OldValues = changes.oldValues;
            NewValues = changes.newValues;
            UserId = userId;
            EventDescription = theEvent.EventDescription;
        }

        public Guid Id { get; set; }

        public string Data { get; private set; }

        public string OldValues { get; private set; }

        public string NewValues { get; private set; }

        public Guid UserId { get; private set; }

        public EventLog()
        {

        }
    }
}