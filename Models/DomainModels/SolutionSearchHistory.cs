using System;

namespace IST.Models.DomainModels
{
    public class SolutionSearchHistory : BaseModel
    {
        public int SolutionSearchHistoryId { get; set; }
        public int SolutionId { get; set; }
        public DateTime SearchTime { get; set; }
        public string SearchString { get; set; }
    }
}
