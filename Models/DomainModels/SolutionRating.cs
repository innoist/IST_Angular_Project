using IST.Models.IdentityModels;

namespace IST.Models.DomainModels
{
    public class SolutionRating : BaseModel
    {
        public int RatingId { get; set; }
        public int SolutionId { get; set; }
        public int Rating { get; set; }
        public string UserId { get; set; }
        public string Comments { get; set; }
        public virtual Solution Solution { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
