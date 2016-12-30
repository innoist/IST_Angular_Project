using System;
using System.ComponentModel.DataAnnotations;

namespace IST.WebApi2.Models
{
    public class SchoolGroupModel : BaseModel
    {
        public int SchoolGroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
