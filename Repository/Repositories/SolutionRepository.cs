using System.Data.Entity;
using IST.Interfaces.Repository;
using IST.Models.DomainModels;
using IST.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace IST.Repository.Repositories
{
    public class SolutionRepository : BaseRepository<Solution>, ISolutionRepository
    {
        public SolutionRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<Solution> DbSet => db.Solutions;
    }
}
