using System.Collections.Generic;
using IST.Models.DomainModels;

namespace IST.Interfaces.IServices
{
    public interface ISolutionOwnerService
    {
        IEnumerable<SolutionOwner> GetAll();
        SolutionOwner FindSolutionOwnerById(int solutionOwnerId);
        bool SaveSolutionOwner(SolutionOwner solutionOwner);
        bool UpdateSolutionOwner(SolutionOwner solutionOwner);
        bool DeleteSolutionOwner(int solutionOwnerId);
    }
}
