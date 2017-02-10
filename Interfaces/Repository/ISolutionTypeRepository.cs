using System.Collections.Generic;
using IST.Models.DomainModels;

namespace IST.Interfaces.Repository
{
    public interface ISolutionTypeRepository : IBaseRepository<SolutionType, long>
    {
        SolutionType FindSolutionTypeById(int solutionTypeId);
        IEnumerable<SolutionType> GetAllSolutionTypes();
    }
}
