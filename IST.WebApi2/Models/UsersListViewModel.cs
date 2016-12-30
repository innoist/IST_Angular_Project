using System.Collections.Generic;

namespace IST.WebApi2.Models
{
    public class UsersListViewModel
    {
        public UsersListViewModel()
        {
            Data = new List<UsersModel>();
        }

        public IEnumerable<UsersModel> Data { get; set; }

        public int TotalCount { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
    }
}