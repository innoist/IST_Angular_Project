using System;
using System.Collections.Generic;
using IST.Models.Common;

namespace IST.Models.RequestModels
{
    public class SolutionSearchRequest : GetPagedListRequest
    {
        public int SolutionId { get; set; }
        public string Name { get; set; }
        public int? TypeId { get; set; }
        public int? OwnerId { get; set; }
        public List<int> FilterIds { get; set; }
        public string ClientRequest { get; set; }
        public bool IsFavorite { get; set; }
        public OrderBySolution OrderByColumn
        {
            get
            {
                return (OrderBySolution)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
