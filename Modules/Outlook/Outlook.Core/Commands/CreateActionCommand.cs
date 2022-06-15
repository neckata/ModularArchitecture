using MediatR;
using ModularArchitecture.DTOs.Actions;
using ModularArchitecture.Shared.Core.Wrapper;
using Outlook.Core.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Outlook.Core.Commands
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
        private IOutlookClient _outlookClient;

        public CreateActionCommandHandler(IOutlookClient outlookClient)
        {
            _outlookClient = outlookClient;
        }

        public async Task<IResult<Guid>> Handle(CreateActionCommand command, CancellationToken cancellationToken)
        {
            return await _outlookClient.CreateActionAsync(command.Request);
        }
    }
}
