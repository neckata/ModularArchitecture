using MediatR;
using ModularArchitecture.Shared.Core.Entities;
using ModularArchitecture.Shared.Core.Wrapper;
using Outlook.Core.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Outlook.Core.Commands
{
    /// <summary>
    /// Get actions command, created with reflection
    /// </summary>
    public class GetActionsCommand : IRequest<IResult<List<Action>>>
    {

    }

    /// <summary>
    /// Outlook handler for get actions command, called with Reflection
    /// </summary>
    public class GetActionsCommandHandler : IRequestHandler<GetActionsCommand, IResult<List<Action>>>
    {
        private readonly IOutlookClient _outlookClient;

        /// <summary>
        /// GetActionsCommandHandler
        /// </summary>
        /// <param name="outlookClient"></param>
        public GetActionsCommandHandler(IOutlookClient outlookClient)
        {
            _outlookClient = outlookClient;
        }

        /// <summary>
        /// Handle GetActionsCommand
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IResult<List<Action>>> Handle(GetActionsCommand command, CancellationToken cancellationToken)
        {
            return await _outlookClient.GetActionsAsync();
        }
    }
}
