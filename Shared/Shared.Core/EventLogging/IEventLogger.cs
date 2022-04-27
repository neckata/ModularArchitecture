using System.Threading.Tasks;
using Gamification.Shared.Core.Domain;
using Gamification.Shared.Core.Interfaces;

namespace Gamification.Shared.Core.EventLogging
{
    public interface IEventLogger
    {
        Task SaveAsync<T>(T @event, (string oldValues, string newValues) changes, IApplicationDbContext context)
            where T : Event;
    }
}