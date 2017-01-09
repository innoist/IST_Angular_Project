using System.Data.Entity;
using IST.Interfaces.Repository;
using IST.Models.DomainModels;
using IST.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace IST.Repository.Repositories
{
    public class SolutionTypeRepository : BaseRepository<SolutionType>, ISolutionTypeRepository
    {
        public SolutionTypeRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<SolutionType> DbSet => db.SolutionTypes;
    }
}
