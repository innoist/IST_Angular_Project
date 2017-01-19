using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IST.WebApi2.Models
{
    public class TagModel
    {
        public int Id { get; set; }
        public string DisplayValue { get; set; }
        public int TagGroupId { get; set; }
        public string TagGroupName { get; set; }
    }
}