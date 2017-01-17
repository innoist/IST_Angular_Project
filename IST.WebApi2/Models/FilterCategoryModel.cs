using System.Collections.Generic;
using IST.Models.DomainModels;

namespace IST.WebApi2.Models
{
    public class FilterCategoryModel
    {
        public int Id { get; set; }
        public string DisplayValue { get; set; }
        public virtual ICollection<FilterModel> Filters { get; set; }
    }
}