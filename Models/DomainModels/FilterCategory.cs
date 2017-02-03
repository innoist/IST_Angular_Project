using System.Collections.Generic;

namespace IST.Models.DomainModels
{
    public class FilterCategory : BaseModel
    {
        public int Id { get; set; }
        public string DisplayValue { get; set; }
        public virtual ICollection<Filter> Filters { get; set; }
    }
}
