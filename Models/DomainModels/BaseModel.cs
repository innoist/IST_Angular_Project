using System;
using IST.Models.IdentityModels;

namespace IST.Models.DomainModels
{
    public class BaseModel
    {
        public string RecCreatedById { get; set; }
        public DateTime RecCreatedOn { get; set; }
        public string RecLastUpdatedById { get; set; }
        public DateTime RecLastUpdatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public virtual AspNetUser CreatedByUser { get; set; }
        public virtual AspNetUser UpdatedByUser { get; set; }
    }
}
