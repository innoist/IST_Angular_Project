using IST.Models.IdentityModels;

namespace IST.Models.DomainModels
{
    public class UserDetail
    {
        public int UserDetailId { get; set; }
        public string CompanyName { get; set; }
        public string CountryName { get; set; }
        public string Address { get; set; }
        public string AccountType { get; set; }
        public string UserId { get; set; }
        public string CompanyShortUrl { get; set; }
        public virtual AspNetUser User { get; set; }

    }
}
