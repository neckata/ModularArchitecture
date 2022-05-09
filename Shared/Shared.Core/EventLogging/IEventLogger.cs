using System.Threading.Tasks;
using ModularArchitecture.Shared.Core.Domain;
using ModularArchitecture.Shared.Core.Interfaces;

namespace ModularArchitecture.Shared.Core.EventLogging
{
    public interface IEventLogger
    {
        Task SaveAsync<T>(T @event, (string oldValues, string newValues) changes, IApplicationDbContext context)
            where T : Event;
    }
}