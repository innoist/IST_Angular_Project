using System.Collections.Generic;
using IST.Models.IdentityModels;

namespace IST.Models.ResponseModels
{
    public class UsersSearchResponse
    {

        public UsersSearchResponse()
        {
            Users = new List<AspNetUser>();
        }

        public IEnumerable<AspNetUser> Users { get; set; }

        public int TotalCount { get; set; }
        public int TotalRecords { get; set; }
        public int FilteredCount { get; set; }
    }
}
