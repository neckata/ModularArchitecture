using Gamification.Shared.DTOs.Connector;
using Gamification.Shared.Core.Interfaces.Services.Connector;
using Gamification.Shared.Core.Wrapper;
using System.Threading.Tasks;
using AutoMapper;
using Gamification.Shared.Core.Features;
using Gamification.Shared.Core.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IResult<string>> UpdateAsync(UpdateConnectorRequest request)
        {
            var connector = await _context.Connectors.Where(b => b.Id == request.Id).AsNoTracking().FirstOrDefaultAsync();

            _mapper.Map(request, connector);

            //Example of adding domain event
            connector.AddDomainEvent(new ConnectorUpdatedEvent(connector));

            _context.Connectors.Update(connector);
            await _context.SaveChangesAsync();

            return await Result<string>.SuccessAsync(connector.Id, "Connector Updated");
        }

        public async Task<IResult<string>> GetEvents()
        {
            return await Result<string>.SuccessAsync();
        }
    }
}
