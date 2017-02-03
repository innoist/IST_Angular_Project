using System.Data.Entity;
using System.Linq;
using IST.Interfaces.Repository;
using IST.Models.DomainModels;
using IST.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace IST.Repository.Repositories
{
    public class UsageHistoryRepository : BaseRepository<SolutionUsageHistory>, IUsageHistoryRepository
    {
        public UsageHistoryRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<SolutionUsageHistory> DbSet => db.SolutionUsageHistories;
        public SolutionUsageHistory GetLinkByExternalClick(string userId, string email, int solutionId)
        {
            return DbSet.FirstOrDefault(x=>x.SolutionId == solutionId && x.RecCreatedById == userId);
        }
    }
}
