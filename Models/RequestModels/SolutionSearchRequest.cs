using System;
using IST.Models.Common;

namespace IST.Models.RequestModels
{
    public class SolutionSearchRequest : GetPagedListRequest
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public OrderByProject OrderByColumn
        {
            get
            {
                return (OrderByProject)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
