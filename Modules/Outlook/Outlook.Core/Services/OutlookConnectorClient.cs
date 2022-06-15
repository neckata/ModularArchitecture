﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ModularArchitecture.DTOs.Actions;
using ModularArchitecture.Shared.Core.Entities;
using ModularArchitecture.Shared.Core.Features;
using ModularArchitecture.Shared.Core.Interfaces;
using ModularArchitecture.Shared.Core.Wrapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Outlook.Core.Services
{
    public class OutlookConnectorClient : IOutlookClient
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public OutlookConnectorClient(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IResult<List<Action>>> GetActionsAsync()
        {
            var actions = await _context.Actions.Where(x=>x.ConnectorType == "Outlook").AsNoTracking().ToListAsync();

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

            action.ConnectorType = "Outlook";

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
