namespace IST.WebApi2.Models
{
    public class SolutionRatingModel:BaseModel
    {
        public int RatingId { get; set; }
        public int SolutionId { get; set; }
        public int? Rating { get; set; }
        public int? ReplyParentId { get; set; }
        public bool? IsReply { get; set; }
        public string Comments { get; set; }
        public string Username { get; set; }
    }
}
