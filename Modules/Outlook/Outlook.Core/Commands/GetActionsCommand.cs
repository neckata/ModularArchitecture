using MediatR;
using ModularArchitecture.Shared.Core.Entities;
using ModularArchitecture.Shared.Core.Wrapper;
using Outlook.Core.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Outlook.Core.Commands
{
    public class GetActionsCommand : IRequest<IResult<List<Action>>>
    {

    }

    public class GetActionsCommandHandler : IRequestHandler<GetActionsCommand, IResult<List<Action>>>
    {
        private readonly IOutlookClient _outlookClient;

        public GetActionsCommandHandler(IOutlookClient outlookClient)
        {
            _outlookClient = outlookClient;
        }

        public async Task<IResult<List<Action>>> Handle(GetActionsCommand command, CancellationToken cancellationToken)
        {
            return await _outlookClient.GetActionsAsync();
        }
    }
}
