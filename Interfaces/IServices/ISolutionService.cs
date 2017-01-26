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
        SearchTemplateResponse<Solution> Search(SolutionSearchRequest searchRequest);
        Solution GetById(int id);
        SolutionBaseData GetBaseData(int id);
        ProjectBaseData GetProjectBaseData(int id);
        SolutionBaseData GetFilterData();
        bool SaveOrUpdate(SolutionCreateResponseModel response);
        IEnumerable<Solution> SearchForTypeAhead(string name, List<int> filterIds);
        bool SaveFavorite(int solutionId, bool saveOrDelete);
        bool DeleteSolution(int solutionId);
    }
}
