using IST.Models.DomainModels;

namespace IST.Interfaces.IServices
{
    public interface IUsageHistoryService
    {
        bool SaveUsage(int solutionId, int usageType, string email, string emailBody);
    }
}
