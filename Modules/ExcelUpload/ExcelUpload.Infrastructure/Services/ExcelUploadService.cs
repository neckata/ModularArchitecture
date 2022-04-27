using Gamification.Shared.DTOs.Connector;
using Gamification.Shared.Core.Interfaces.Services.Connector;
using Gamification.Shared.Core.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gamification.Shared.Core.Entities;
using AutoMapper;
using Gamification.Shared.Core.Features;
using Gamification.Shared.Core.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ExcelUpload.Infrastructure.Services
{
    public class ExcelUploadService : IConnectorService
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public ExcelUploadService(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public Task<Result<List<ConnectorResponse>>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<IResult<ConnectorResponse>> GetAsync(string userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IResult<string>> UpdateAsync(UpdateConnectorRequest request)
        {
            var connector = await _context.Connectors.Where(b => b.Id == request.Id).AsNoTracking().FirstOrDefaultAsync();

            _mapper.Map(request, connector);

            //Example of adding domain event
            connector.AddDomainEvent(new ConnectorUpdatedEvent(connector));

            throw new System.NotImplementedException();
        }
    }
}
