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
using ExcelUpload.Core.Interfaces;
using Gamification.Shared.Core.Exceptions;

namespace ExcelUpload.Core.Services
{
    public class ExcelUploadConnectorClient : IConnectorClient, IExcelUploadClient
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public ExcelUploadConnectorClient(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IResult<List<Action>>> GetActions()
        {
            var actions = await _context.Actions.Where(x => x.ConnectorType == ConnectorTypeEnum.ExcelUpload).AsNoTracking().ToListAsync();

            return await Result<List<Action>>.SuccessAsync(actions);
        }

        public async Task<IResult<System.Guid>> UpdateActionAsync(UpdateActionRequest request)
        {
            UpdateExcel(request);

            var action = await _context.Actions.Where(b => b.Id == request.Id).AsNoTracking().FirstOrDefaultAsync();

            _mapper.Map(request, action);

            action.AddDomainEvent(new ActionUpdatedEvent(action));

            _context.Actions.Update(action);
            await _context.SaveChangesAsync();

            return await Result<System.Guid>.SuccessAsync(action.Id, "Action Updated");
        }

        /// <summary>
        /// Not implemented for excel upload
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IResult<System.Guid>> CreateActionAsync(CreateActionRequest request)
        {
            //There is no need of this implemantation
            throw new MethodNotImplementedException("ExcelUpload can't create action from method");
        }

        public async Task<IResult<System.Guid>> UploadFile(CreateActionRequest createActionRequest)
        {
            AddExcel(createActionRequest);

            //Read excel fille

            var action = _mapper.Map<Action>(createActionRequest);

            action.ConnectorType = ConnectorTypeEnum.ExcelUpload;

            action.AddDomainEvent(new ActionAddEvent(action));

            await _context.Actions.AddAsync(action);
            await _context.SaveChangesAsync();

            return await Result<System.Guid>.SuccessAsync(action.Id, "Action Added");
        }

        private void UpdateExcel(UpdateActionRequest request)
        {
            //Here you will update the excel file in the file system
        }

        private void AddExcel(CreateActionRequest createActionRequest)
        {
            //Here you add the exel to the file system
        } 
    }
}
