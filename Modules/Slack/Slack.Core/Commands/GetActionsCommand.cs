using MediatR;
using ModularArchitecture.Shared.Core.Entities;
using ModularArchitecture.Shared.Core.Wrapper;
using Slack.Core.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Slack.Core.Commands
{
    /// <summary>
    /// Get actions command, created with reflection
    /// </summary>
    public class GetActionsCommand : IRequest<IResult<List<Action>>>
    {

    }

    /// <summary>
    /// Slack handler for get actions command, called with Reflection
    /// </summary>
    public class GetActionsCommandHandler : IRequestHandler<GetActionsCommand, IResult<List<Action>>>
    {
        private readonly ISlackClient _slackClient;

        /// <summary>
        /// GetActionsCommandHandler
        /// </summary>
        /// <param name="slackClient"></param>
        public GetActionsCommandHandler(ISlackClient slackClient)
        {
            _slackClient = slackClient;
        }

        /// <summary>
        /// Handle GetActionsCommand
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IResult<List<Action>>> Handle(GetActionsCommand command, CancellationToken cancellationToken)
        {
            return await _slackClient.GetActionsAsync();
        }
    }
}
