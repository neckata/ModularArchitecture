﻿using ModularArchitecture.Shared.Core.Interfaces.Services.Connector;
using ModularArchitecture.Shared.Core.Wrapper;
using System.Threading.Tasks;
using AutoMapper;
using ModularArchitecture.Shared.Core.Features;
using ModularArchitecture.Shared.Core.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ModularArchitecture.DTOs.Actions;
using ModularArchitecture.Shared.Core.Entities;
using System.Collections.Generic;
using Slack.Core.Interfaces;

namespace Slack.Core.Services
{
    public class SlackConnectorClient : ISlackClient
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public SlackConnectorClient(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IResult<List<Action>>> GetActionsAsync()
        {
            var actions = await _context.Actions.Where(x => x.ConnectorType == "Slack").AsNoTracking().ToListAsync();

            return await Result<List<Action>>.SuccessAsync(actions);
        }

        public async Task<IResult<System.Guid>> UpdateActionAsync(UpdateActionRequest request)
        {
            UpdateChannel(request);

            var action = await _context.Actions.Where(b => b.Id == request.Id).AsNoTracking().FirstOrDefaultAsync();

            _mapper.Map(request, action);

            action.AddDomainEvent(new ActionUpdatedEvent(action));

            _context.Actions.Update(action);
            await _context.SaveChangesAsync();

            return await Result<System.Guid>.SuccessAsync(action.Id, "Action Updated");
        }

        public async Task<IResult<System.Guid>> CreateActionAsync(CreateActionRequest request)
        {
            AddChannel(request);

            var action = _mapper.Map<Action>(request);

            action.ConnectorType = "Slack";

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
