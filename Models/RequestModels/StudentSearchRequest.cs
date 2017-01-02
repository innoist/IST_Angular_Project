using System;
using IST.Models.Common;

namespace IST.Models.RequestModels
{
    public class StudentSearchRequest : GetPagedListRequest
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public DateTime ? DateOfBirth { get; set; }
        public string GuardianPhone { get; set; }
        public OrderByStudent OrderByColumn
        {
            get
            {
                return (OrderByStudent)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
