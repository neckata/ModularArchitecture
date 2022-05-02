using System.Threading.Tasks;
using Gamification.Shared.Core.Wrapper;

namespace Outlook.Infrastructure.Services
{
    public interface IOutlookService
    {
        public Task<IResult<string>> GetEvents();
    }
}
