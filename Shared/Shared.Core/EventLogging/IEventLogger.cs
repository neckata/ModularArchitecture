using System.Threading.Tasks;
using Gamification.Shared.Core.Domain;

namespace Gamification.Shared.Core.EventLogging
{
    public interface IEventLogger
    {
        Task SaveAsync<T>(T @event, (string oldValues, string newValues) changes)
            where T : Event;
    }
}