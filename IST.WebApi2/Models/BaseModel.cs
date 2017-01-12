using System;
using IST.Models.IdentityModels;

namespace IST.WebApi2.Models
{
    public class BaseModel
    {
        public string RecCreatedById { get; set; }
        public DateTime RecCreatedOn { get; set; }
        public string RecLastUpdatedById { get; set; }
        public DateTime RecLastUpdatedOn { get; set; }
    }
}
