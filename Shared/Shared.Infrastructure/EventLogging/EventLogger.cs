using System.Threading.Tasks;
using Gamification.Shared.Core.Domain;
using Gamification.Shared.Core.EventLogging;
using Gamification.Shared.Core.Interfaces;
using Gamification.Shared.Core.Interfaces.Serialization;
using Gamification.Shared.Core.Interfaces.Services.Identity;

namespace Gamification.Shared.Infrastructure.EventLogging
{
    internal class EventLogger : IEventLogger
    {
        private readonly ICurrentUser _user;
        private readonly IApplicationDbContext _context;
        private readonly IJsonSerializer _jsonSerializer;

        public EventLogger(
            ICurrentUser user,
            IApplicationDbContext context,
            IJsonSerializer jsonSerializer)
        {
            _user = user;
            _context = context;
            _jsonSerializer = jsonSerializer;
        }

        public async Task SaveAsync<T>(T @event, (string oldValues, string newValues) changes)
            where T : Event
        {
            if (@event is EventLog eventLog)
            {
                await _context.EventLogs.AddAsync(eventLog);
                await _context.SaveChangesAsync();
            }
            else
            {
                string serializedData = _jsonSerializer.Serialize(@event, @event.GetType());

                var userId = _user.GetUserId();
                var thisEvent = new EventLog(
                    @event,
                    serializedData,
                    changes,
                    userId);
                await _context.EventLogs.AddAsync(thisEvent);
                await _context.SaveChangesAsync();
            }
        }
    }
}