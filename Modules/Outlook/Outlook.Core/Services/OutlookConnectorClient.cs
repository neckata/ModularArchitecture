using Gamification.Shared.Core.Interfaces.Services.Connector;
using Gamification.Shared.Core.Wrapper;
using System.Threading.Tasks;
using AutoMapper;
using Gamification.Shared.Core.Features;
using Gamification.Shared.Core.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Gamification.DTOs.Actions;
using Gamification.Shared.Core.Entities;
using System.Collections.Generic;
using Gamification.Shared.Core.Enums;

namespace Outlook.Core.Services
{
    public class OutlookConnectorClient : IConnectorClient, IOutlookClient
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public OutlookConnectorClient(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IResult<List<Action>>> GetActions()
        {
            var actions = await _context.Actions.Where(x=>x.ConnectorType == ConnectorTypeEnum.Outlook).AsNoTracking().ToListAsync();

            return await Result<List<Action>>.SuccessAsync(actions);
        }

        public async Task<IResult<System.Guid>> UpdateActionAsync(UpdateActionRequest request)
        {
            UpdateOutlookEvent(request);

            var action = await _context.Actions.Where(b => b.Id == request.Id).AsNoTracking().FirstOrDefaultAsync();

            _mapper.Map(request, action);

            //Example of adding domain event
            action.AddDomainEvent(new ActionUpdatedEvent(action));

            _context.Actions.Update(action);
            await _context.SaveChangesAsync();

            return await Result<System.Guid>.SuccessAsync(action.Id, "Action Updated");
        }

        public async Task<IResult<System.Guid>> CreateActionAsync(CreateActionRequest request)
        {
            AddOutlookEvent(request);

            var action = _mapper.Map<Action>(request);

            action.ConnectorType = ConnectorTypeEnum.Outlook;

            action.AddDomainEvent(new ActionAddEvent(action));

            await _context.Actions.AddAsync(action);
            await _context.SaveChangesAsync();

            return await Result<System.Guid>.SuccessAsync(action.Id, "Action Added");
        }

        private void UpdateOutlookEvent(UpdateActionRequest request)
        {
            //Here you will connect and update the event in outlook
        }

        private void AddOutlookEvent(CreateActionRequest request)
        {
            //Here you will connect and add the event in outlook
        }
    }
}
