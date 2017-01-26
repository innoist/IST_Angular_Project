using System.Collections.Generic;
using IST.Models.Common.DropDown;

namespace IST.WebApi2.Models
{
    public class FilterModel:BaseModel
    {
        public int Id { get; set; }
        public int FilterCategoryId { get; set; }
        public string DisplayValue { get; set; }
        public int SolutionCount { get; set; }
        public string FilterCategoryName { get; set; }
    }

    public class FilterViewModel
    {
        public FilterModel FilterModel { get; set; }
        public List<DropDownModel> FilterCategories { get; set; } 
    }
}