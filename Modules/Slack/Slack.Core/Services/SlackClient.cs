using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ModularArchitecture.DTOs.Actions;
using ModularArchitecture.Shared.Core.Entities;
using ModularArchitecture.Shared.Core.Features;
using ModularArchitecture.Shared.Core.Interfaces;
using ModularArchitecture.Shared.Core.Wrapper;
using Slack.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slack.Core.Services
{
    /// <summary>
    /// SlackClient extends IModuleClient
    /// </summary>
    public class SlackClient : ISlackClient
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        /// <summary>
        /// SlackClient
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="context"></param>
        public SlackClient(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Get created actions in slack
        /// </summary>
        /// <returns></returns>
        public async Task<IResult<List<Action>>> GetActionsAsync()
        {
            List<Action> actions = await _context.Actions.Where(x => x.ModuleType == "Slack").AsNoTracking().ToListAsync();

            return await Result<List<Action>>.SuccessAsync(actions);
        }

        /// <summary>
        /// Update action in slack
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IResult<System.Guid>> UpdateActionAsync(UpdateActionRequest request)
        {
            UpdateChannel(request);

            Action action = await _context.Actions.Where(b => b.Id == request.Id).AsNoTracking().FirstOrDefaultAsync();

            _mapper.Map(request, action);

            action.AddDomainEvent(new ActionUpdatedEvent(action));

            _context.Actions.Update(action);
            await _context.SaveChangesAsync();

            return await Result<System.Guid>.SuccessAsync(action.Id, "Action Updated");
        }

        /// <summary>
        /// Create action in slack
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IResult<System.Guid>> CreateActionAsync(CreateActionRequest request)
        {
            AddChannel(request);

            Action action = _mapper.Map<Action>(request);

            action.ModuleType = "Slack";

            action.AddDomainEvent(new ActionAddEvent(action));

            await _context.Actions.AddAsync(action);
            await _context.SaveChangesAsync();

            return await Result<System.Guid>.SuccessAsync(action.Id, "Channel Added");
        }

        private void UpdateChannel(UpdateActionRequest request)
        {
            //Here you will update the channel
        }

        private void AddChannel(CreateActionRequest createActionRequest)
        {
            //Here you add the channel
        }
    }
}
