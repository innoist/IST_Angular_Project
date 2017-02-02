using IST.Models.DomainModels;
using IST.Models.RequestModels;
using IST.Models.ResponseModels;

namespace IST.Interfaces.IServices
{
    /// <summary>
    /// Interface for Solution rating Service
    /// </summary>
    public interface ISolutionRatingService
    {
        SearchTemplateResponse<Solution> Search(SolutionSearchRequest searchRequest);
        Solution GetById(int id);
        double? SaveOrUpdate(SolutionRating response);
    }
}
