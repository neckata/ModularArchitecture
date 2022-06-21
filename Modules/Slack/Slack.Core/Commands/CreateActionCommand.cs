using MediatR;
using ModularArchitecture.DTOs.Actions;
using ModularArchitecture.Shared.Core.Wrapper;
using Slack.Core.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Slack.Core.Commands
{
    /// <summary>
    /// Create action command, created with reflection
    /// </summary>
    public class CreateActionCommand : IRequest<IResult<Guid>>
    {
        /// <summary>
        /// Request
        /// </summary>
        public CreateActionRequest Request { get; set; }

        /// <summary>
        /// CreateActionCommand
        /// </summary>
        /// <param name="createAction"></param>
        public CreateActionCommand(CreateActionRequest createAction)
        {
            Request = createAction;
        }
    }

    /// <summary>
    /// Slack handler for create action command, called with Reflection
    /// </summary>
    public class CreateActionCommandHandler : IRequestHandler<CreateActionCommand, IResult<Guid>>
    {
        private readonly ISlackClient _slackClient;

        /// <summary>
        /// CreateActionCommandHandler
        /// </summary>
        /// <param name="slackClient"></param>
        public CreateActionCommandHandler(ISlackClient slackClient)
        {
            _slackClient = slackClient;
        }

        /// <summary>
        /// Handle CreateActionCommand
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IResult<Guid>> Handle(CreateActionCommand command, CancellationToken cancellationToken)
        {
            return await _slackClient.CreateActionAsync(command.Request);
        }
    }
}
