using MediatR;
using ModularArchitecture.Shared.Core.Entities;
using ModularArchitecture.Shared.Core.Wrapper;
using Slack.Core.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Slack.Core.Commands
{
    public class GetActionsCommand : IRequest<IResult<List<Action>>>
    {

    }

    public class GetActionsCommandHandler : IRequestHandler<GetActionsCommand, IResult<List<Action>>>
    {
        private readonly ISlackClient _slackClient;

        public GetActionsCommandHandler(ISlackClient slackClient)
        {
            _slackClient = slackClient;
        }

        public async Task<IResult<List<Action>>> Handle(GetActionsCommand command, CancellationToken cancellationToken)
        {
            return await _slackClient.GetActionsAsync();
        }
    }
}
