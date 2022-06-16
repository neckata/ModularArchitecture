using MediatR;
using ModularArchitecture.DTOs.Actions;
using ModularArchitecture.Shared.Core.Wrapper;
using Slack.Core.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Slack.Core.Commands
{
    public class CreateActionCommand : IRequest<IResult<Guid>>
    {
        public CreateActionRequest Request { get; set; }

        public CreateActionCommand(CreateActionRequest createAction)
        {
            Request = createAction;
        }
    }

    public class CreateActionCommandHandler : IRequestHandler<CreateActionCommand, IResult<Guid>>
    {
        private ISlackClient _slackClient;

        public CreateActionCommandHandler(ISlackClient slackClient)
        {
            _slackClient = slackClient;
        }

        public async Task<IResult<Guid>> Handle(CreateActionCommand command, CancellationToken cancellationToken)
        {
            return await _slackClient.CreateActionAsync(command.Request);
        }
    }
}
