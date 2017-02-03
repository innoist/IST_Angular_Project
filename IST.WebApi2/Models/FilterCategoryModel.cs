using System.Collections.Generic;

namespace IST.WebApi2.Models
{
    public class FilterCategoryModel : BaseModel
    {
        public int Id { get; set; }
        public string DisplayValue { get; set; }
        public virtual ICollection<FilterModel> Filters { get; set; }
    }
}