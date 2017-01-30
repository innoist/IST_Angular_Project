using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using IST.Models.IdentityModels;

namespace IST.Models.DomainModels
{
    public class Solution : BaseModel
    {
        public int Id { get; set; }
        public int? MaintentanceHours { get; set; }
        public int OwnerId { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string SecurityInfo { get; set; }
        public string Image { get; set; }
        public bool? Active { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Filter> Filters { get; set; }
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
        public virtual ICollection<SolutionRating> SolutionRatings { get; set; }
        public virtual SolutionOwner SolutionOwner { get; set; }
        public virtual SolutionType SolutionType { get; set; }
        public virtual ICollection<SolutionSearchHistory> SolutionSearchHistories { get; set; }
        public virtual ICollection<SolutionUsageHistory> SolutionUsageHistories { get; set; }
    }
}
