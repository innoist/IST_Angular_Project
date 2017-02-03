using IST.Models.DomainModels;

namespace IST.Interfaces.Repository
{
    public interface IUsageHistoryRepository : IBaseRepository<SolutionUsageHistory, long>
    {
        SolutionUsageHistory GetLinkByExternalClick(string userId, string email, int solutionId);
    }
}
