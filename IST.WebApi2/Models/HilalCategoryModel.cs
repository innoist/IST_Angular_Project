using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IST.WebApi2.Models
{
    public class HilalCategoryModel:BaseDayCareGroup
    {
        public int HilalCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}