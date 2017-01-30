using System.Collections.Generic;
using IST.Models.IdentityModels;

namespace IST.Models.DomainModels
{
    public class SolutionRating : BaseModel
    {
        public int RatingId { get; set; }
        public int SolutionId { get; set; }
        public int? Rating { get; set; }
        public int? ReplyParentId { get; set; }
        public bool? IsReply { get; set; }
        public string Comments { get; set; }
        public virtual Solution Solution { get; set; }
        public virtual SolutionRating ParentItem { get; set; } 
        public virtual ICollection<SolutionRating> SolutionRatings { get; set; } 
    }
}
