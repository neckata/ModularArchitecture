using System.Threading.Tasks;
using ModularArchitecture.DTOs.Actions;
using ModularArchitecture.Shared.Core.Wrapper;

namespace ExcelUpload.Core.Interfaces
{
    public interface IExcelUploadClient
    {
        public Task<IResult<System.Guid>> UploadFile(CreateActionRequest createActionRequest);
    }
}
