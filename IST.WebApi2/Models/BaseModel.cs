using System;

namespace IST.WebApi2.Models
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

    public class BaseDayCareGroup : BaseModel
    {
        public int DayCareGroupId { get; set; }
    }

    public class BaseDayCare : BaseModel
    {
        public int DayCareId { get; set; }
    }
}
