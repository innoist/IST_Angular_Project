using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using IST.Interfaces.Repository;
using IST.Models.DomainModels;
using IST.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace IST.Repository.Repositories
{
    public class SolutionOwnerRepository : BaseRepository<SolutionOwner>, ISolutionOwnerRepository
    {
        public SolutionOwnerRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<SolutionOwner> DbSet => db.SolutionOwners;

        public SolutionOwner FindSolutionOwnerById(int solutionOwnerId)
        {
            return DbSet.FirstOrDefault(x => x.Id == solutionOwnerId && !x.IsDeleted);
        }

        public IEnumerable<SolutionOwner> GetAllSolutionOwners()
        {
            return DbSet.Where(x => !x.IsDeleted);
        }
    }
}
