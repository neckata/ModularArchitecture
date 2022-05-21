using System.Collections.Generic;
using System.Threading.Tasks;
using ModularArchitecture.Shared.Core.Wrapper;

namespace Slack.Core.Interfaces
{
    public interface ISlackClient
    {
        public Task<IResult<List<string>>> GetChannels();
    }
}
