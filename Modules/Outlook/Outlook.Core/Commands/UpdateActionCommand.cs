using MediatR;
using ModularArchitecture.DTOs.Actions;
using ModularArchitecture.Shared.Core.Wrapper;
using Outlook.Core.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Outlook.Core.Commands
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
        private readonly IOutlookClient _outlookClient;

        public UpdateActionCommandHandler(IOutlookClient outlookClient)
        {
            _outlookClient = outlookClient;
        }

        public async Task<IResult<Guid>> Handle(UpdateActionCommand command, CancellationToken cancellationToken)
        {
            return await _outlookClient.UpdateActionAsync(command.Request);
        }
    }
}
