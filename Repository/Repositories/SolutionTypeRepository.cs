using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public SolutionType FindSolutionTypeById(int solutionTypeId)
        {
            return DbSet.FirstOrDefault(x => x.Id == solutionTypeId && !x.IsDeleted);
        }

        public IEnumerable<SolutionType> GetAllSolutionTypes()
        {
            return DbSet.Where(x => !x.IsDeleted);
        }
    }
}
