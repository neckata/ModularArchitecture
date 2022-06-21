using MediatR;
using ModularArchitecture.DTOs.Actions;
using ModularArchitecture.Shared.Core.Wrapper;
using Outlook.Core.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Outlook.Core.Commands
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
    /// Outlook handler for create action command, called with Reflection
    /// </summary>
    public class CreateActionCommandHandler : IRequestHandler<CreateActionCommand, IResult<Guid>>
    {
        private readonly IOutlookClient _outlookClient;

        /// <summary>
        /// CreateActionCommandHandler
        /// </summary>
        /// <param name="outlookClient"></param>
        public CreateActionCommandHandler(IOutlookClient outlookClient)
        {
            _outlookClient = outlookClient;
        }

        /// <summary>
        /// Handle CreateActionCommand
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IResult<Guid>> Handle(CreateActionCommand command, CancellationToken cancellationToken)
        {
            return await _outlookClient.CreateActionAsync(command.Request);
        }
    }
}
