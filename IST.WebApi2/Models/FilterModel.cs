using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IST.WebApi2.Models
{
    public class FilterModel
    {
        public int Id { get; set; }
        public int FilterCategoryId { get; set; }
        public string DisplayValue { get; set; }
        public int SolutionCount { get; set; }
        public string FilterCategoryName { get; set; }
    }
}