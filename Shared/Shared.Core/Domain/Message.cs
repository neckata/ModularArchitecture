using Shared.Core.Utilities;
using System;

namespace Shared.Core.Domain
{
    public abstract class Message
    {
        public string MessageType { get; protected set; }

        public Guid AggregateId { get; protected set; }

        protected Message()
        {
            MessageType = GetType().GetGenericTypeName();
        }
    }
}
