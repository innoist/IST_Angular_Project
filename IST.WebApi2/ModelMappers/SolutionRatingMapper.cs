using IST.Models.DomainModels;
using IST.WebApi2.Models;

namespace IST.WebApi2.ModelMappers
{
    public static class SolutionRatingMapper
    {
        public static SolutionRatingModel MapFromServerToClient(this SolutionRating source)
        {
            return new SolutionRatingModel
            {
                RatingId = source.RatingId,
                SolutionId = source.SolutionId,
                UserId = source.UserId,
                Rating = source.Rating,
                Comments = source.Comments
            };
        }
        public static SolutionRating MapFromClientToServer(this SolutionRatingModel source)
        {
            return new SolutionRating
            {
                RatingId = source.RatingId,
                SolutionId = source.SolutionId,
                UserId = source.UserId,
                Rating = source.Rating,
                Comments = source.Comments
            };
        }
        
    }
}
