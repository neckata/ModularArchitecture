using MediatR;
using ModularArchitecture.DTOs.Actions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Outlook.Core.Commands
{
    public class CreateActionCommand : IRequest<string>
    {
        public string Id { get; set; }

        public CreateActionCommand(CreateActionRequest createAction)
        {
            Id = createAction.Id;
        }
    }

    public class CreateActionCommandHandler : IRequestHandler<CreateActionCommand, string>
    {
        public async Task<string> Handle(CreateActionCommand command, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
