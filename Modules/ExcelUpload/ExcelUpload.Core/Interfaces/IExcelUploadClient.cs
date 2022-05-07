using System.Collections.Generic;
using System.Threading.Tasks;
using Gamification.DTOs.Actions;
using Gamification.Shared.Core.Entities;
using Gamification.Shared.Core.Wrapper;

namespace ExcelUpload.Core.Interfaces
{
    public interface IExcelUploadClient
    {
        public Task<IResult<System.Guid>> UploadFile(CreateActionRequest createActionRequest);

        public Task<IResult<List<Action>>> GetActions();
    }
}
