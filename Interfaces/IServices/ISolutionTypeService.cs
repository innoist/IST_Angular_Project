using System.Collections.Generic;
using IST.Models.DomainModels;

namespace IST.Interfaces.IServices
{
    public interface ISolutionTypeService
    {
        IEnumerable<SolutionType> GetAll();
        SolutionType FindSolutionTypeById(int solutionTypeId);
        bool SaveOrUpdate(SolutionType solutionType);
        bool UpdateSolutionType(SolutionType solutionType);
        bool RemoveSolutionType(int solutionTypeId);
        bool DeleteSolutionType(int solutionTypeId);
    }
}
