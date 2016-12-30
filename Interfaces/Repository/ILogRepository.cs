using IST.Models.LoggerModels;
using IST.Models.RequestModels;
using IST.Models.ResponseModels;

namespace IST.Interfaces.Repository
{
    public interface ILogRepository : IBaseRepository<Log, long>
    {
         //SearchTemplateResponse<Log> SearchLogs(LogSearchRequest searchRequest);
    }
}
