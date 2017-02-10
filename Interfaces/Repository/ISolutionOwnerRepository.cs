using System.Collections.Generic;
using IST.Models.DomainModels;

namespace IST.Interfaces.Repository
{
    public interface ISolutionOwnerRepository : IBaseRepository<SolutionOwner, long>
    {
        SolutionOwner FindSolutionOwnerById(int solutionOwnerId);
        IEnumerable<SolutionOwner> GetAllSolutionOwners();
    }
}
