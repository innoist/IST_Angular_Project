using System;
using System.Collections.Generic;
using IST.Models.DomainModels;
using Filter = System.Web.Mvc.Filter;

namespace IST.Models.IdentityModels
{
    /// <summary>
    /// AspNetUser
    /// </summary>
    public partial class AspNetUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Qualification { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public long UserDomainKey { get; set; }
        public long? EmployeeId { get; set; }
        public string Address { get; set; }
        public string CompanyName { get; set; }
        public string ImageName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string UserComments { get; set; }
        public bool HilalOnly { get; set; }
        public int TimeOffset { get; set; }

        public virtual ICollection<UserClaim> AspNetUserClaims { get; set; }
        public virtual ICollection<UserLogin> AspNetUserLogins { get; set; }
        public virtual ICollection<UserRole> AspNetRoles { get; set; }
        public virtual ICollection<DomainModels.Filter> CreatedFilters { get; set; }
        public virtual ICollection<DomainModels.Filter> UpdatedFilters { get; set; }
        public virtual ICollection<FilterCategory> CreatedFilterCategories { get; set; }
        public virtual ICollection<Solution> Solutions { get; set; }
        public virtual ICollection<FilterCategory> UpdatedFilterCategories { get; set; }
        public virtual ICollection<Solution> CreatedSolutions { get; set; }
        public virtual ICollection<Solution> UpdatedSolutions { get; set; }
        public virtual ICollection<SolutionOwner> CreatedSolutionOwners { get; set; }
        public virtual ICollection<SolutionOwner> UpdatedSolutionOwners { get; set; }
        public virtual ICollection<SolutionType> CreatedSolutionTypes { get; set; }
        public virtual ICollection<SolutionType> UpdatedSolutionTypes { get; set; }
        public virtual ICollection<Tag> CreatedTags { get; set; }
        public virtual ICollection<Tag> UpdatedTags { get; set; }
        public virtual ICollection<TagGroup> CreatedTagGroups { get; set; }
        public virtual ICollection<TagGroup> UpdatedTagGroups { get; set; }
        public virtual ICollection<SolutionRating> CreatedSolutionRatings { get; set; }
        public virtual ICollection<SolutionRating> UpdatedSolutionRatings { get; set; }
    }
}
