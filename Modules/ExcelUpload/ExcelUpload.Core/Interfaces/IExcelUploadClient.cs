using System.Threading.Tasks;
using Gamification.Shared.Core.Wrapper;

namespace ExcelUpload.Core.Interfaces
{
    public interface IExcelUploadClient
    {
        public Task<IResult<string>> UploadFile();
    }
}
