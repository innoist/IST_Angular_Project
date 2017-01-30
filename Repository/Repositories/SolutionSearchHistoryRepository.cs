using System.Data.Entity;
using IST.Models.DomainModels;
using IST.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace IST.Repository.Repositories
{
    public class SolutionSearchHistoryRepository : BaseRepository<SolutionSearchHistory>
    {
        public SolutionSearchHistoryRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<SolutionSearchHistory> DbSet => db.SolutionSearchHistories;
    }
}
