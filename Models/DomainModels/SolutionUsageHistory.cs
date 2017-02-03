using System;

namespace IST.Models.DomainModels
{
    public class SolutionUsageHistory : BaseModel
    {
        public int SolutionUsageHistoryId { get; set; }
        public int SolutionId { get; set; }
        public string SentTo { get; set; }
        public bool IsOpened { get; set; }
        public int UsageType { get; set; }
        public DateTime UsageTime { get; set; }
        public virtual Solution Solution { get; set; }
    }
}
