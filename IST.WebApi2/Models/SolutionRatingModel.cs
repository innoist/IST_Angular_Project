namespace IST.WebApi2.Models
{
    public class SolutionRatingModel
    {
        public int RatingId { get; set; }
        public int SolutionId { get; set; }
        public int Rating { get; set; }
        public string UserId { get; set; }
        public string Comments { get; set; }
    }
}
