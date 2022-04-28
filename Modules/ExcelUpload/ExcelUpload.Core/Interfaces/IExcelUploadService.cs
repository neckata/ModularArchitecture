using System.Threading.Tasks;
using Gamification.Shared.Core.Wrapper;

namespace ExcelUpload.Infrastructure.Services
{
    public interface IExcelUploadService
    {
        public Task<IResult<string>> UploadFile();
    }
}
