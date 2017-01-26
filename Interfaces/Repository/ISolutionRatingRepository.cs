using System.Collections.Generic;
using IST.Models.DomainModels;

namespace IST.Interfaces.Repository
{
    public interface ISolutionRatingRepository : IBaseRepository<SolutionRating, long>
    {
        IEnumerable<SolutionRating> GetRatingBySolutionId(int id);
    }
}
