using IST.Models.Common;

namespace IST.Models.RequestModels
{
    public class UsersSearchRequest : GetPagedListRequest
    {
        public string Role { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string UserRole { get; set; }
        public int DomainKey { get; set; }
        public OrderByUsers OrderByColumn
        {
            get
            {
                return (OrderByUsers)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
