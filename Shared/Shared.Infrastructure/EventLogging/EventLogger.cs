using System.Threading.Tasks;
using ModularArchitecture.Shared.Core.Domain;
using ModularArchitecture.Shared.Core.EventLogging;
using ModularArchitecture.Shared.Core.Interfaces;
using ModularArchitecture.Shared.Core.Interfaces.Serialization;
using ModularArchitecture.Shared.Core.Interfaces.Services.Identity;

namespace ModularArchitecture.Shared.Infrastructure.EventLogging
{
    internal class EventLogger : IEventLogger
    {
        private readonly ICurrentUser _user;
        private readonly IJsonSerializer _jsonSerializer;

        public EventLogger(
            ICurrentUser user,
            IJsonSerializer jsonSerializer)
        {
            _user = user;
            _jsonSerializer = jsonSerializer;
        }

        public async Task SaveAsync<T>(T @event, (string oldValues, string newValues) changes, IApplicationDbContext context)
            where T : Event
        {
            if (@event is EventLog eventLog)
            {
                await context.EventLogs.AddAsync(eventLog);
                await context.SaveChangesAsync();
            }
            else
            {
                string serializedData = _jsonSerializer.Serialize(@event, @event.GetType());

                var userId = _user.GetUserId();
                var thisEvent = new EventLog(@event, serializedData, changes, userId);
                await context.EventLogs.AddAsync(thisEvent);
                await context.SaveChangesAsync();
            }
        }
    }
}