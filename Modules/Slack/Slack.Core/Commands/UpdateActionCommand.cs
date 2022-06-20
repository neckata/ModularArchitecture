using MediatR;
using ModularArchitecture.DTOs.Actions;
using ModularArchitecture.Shared.Core.Wrapper;
using Slack.Core.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Slack.Core.Commands
{
    public class UpdateActionCommand : IRequest<IResult<Guid>>
    {
        public UpdateActionRequest Request { get; set; }

        public UpdateActionCommand(UpdateActionRequest UpdateAction)
        {
            Request = UpdateAction;
        }
    }

    public class UpdateActionCommandHandler : IRequestHandler<UpdateActionCommand, IResult<Guid>>
    {
        private readonly ISlackClient _slackClient;

        public UpdateActionCommandHandler(ISlackClient slackClient)
        {
            _slackClient = slackClient;
        }

        public async Task<IResult<Guid>> Handle(UpdateActionCommand command, CancellationToken cancellationToken)
        {
            return await _slackClient.UpdateActionAsync(command.Request);
        }
    }
}
