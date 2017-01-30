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
                Rating = source.Rating,
                Comments = source.Comments,
                IsReply = source.IsReply,
                ReplyParentId = source.ReplyParentId,
                Username = source.CreatedByUser.FirstName + " " + source.CreatedByUser.LastName,
                RecCreatedOn = source.RecCreatedOn
            };
        }
        public static SolutionRating MapFromClientToServer(this SolutionRatingModel source)
        {
            return new SolutionRating
            {
                RatingId = source.RatingId,
                SolutionId = source.SolutionId,
                Rating = source.Rating,
                Comments = source.Comments,
                IsReply = source.IsReply,
                ReplyParentId = source.ReplyParentId,
                RecCreatedById = source.RecCreatedById,
                RecCreatedOn = source.RecCreatedOn,
                RecLastUpdatedById = source.RecLastUpdatedById,
                RecLastUpdatedOn = source.RecLastUpdatedOn
            };
        }

    }
}
