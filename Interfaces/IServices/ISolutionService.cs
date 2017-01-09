using System.Collections.Generic;
using IST.Models.DomainModels;
using IST.Models.RequestModels;
using IST.Models.ResponseModels;

namespace IST.Interfaces.IServices
{
    /// <summary>
    /// Interface for Student Service
    /// </summary>
    public interface ISolutionService
    {
        bool Delete(int id);
        SearchTemplateResponse<Solution> Search(SolutionSearchRequest searchRequest);
        Solution GetById(int id);
    }
}
