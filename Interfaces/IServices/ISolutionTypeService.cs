using System.Collections.Generic;
using IST.Models.DomainModels;

namespace IST.Interfaces.IServices
{
    public interface ISolutionTypeService
    {
        IEnumerable<SolutionType> GetAll();
        SolutionType FindSolutionTypeById(int solutionTypeId);
        bool SaveSolutionType(SolutionType solutionType);
        bool UpdateSolutionType(SolutionType solutionType);
        bool DeleteSolutionType(int solutionTypeId);
    }
}
