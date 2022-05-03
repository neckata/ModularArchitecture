using System.Threading.Tasks;
using Gamification.Shared.Core.Wrapper;

namespace Outlook.Core.Services
{
    public interface IOutlookClient
    {
        public Task<IResult<string>> GetEvents();
    }
}
