using System;

namespace IST.Models.DomainModels
{
    public class BaseModel
    {
        public string RecCreatedById { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedOn { get; set; }
        public string RecLastUpdatedById { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
