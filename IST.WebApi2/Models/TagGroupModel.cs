using System.Collections.Generic;
using IST.Models.DomainModels;

namespace IST.WebApi2.Models
{
    public class TagGroupModel:BaseModel
    {
        public int Id { get; set; }
        public string DisplayValue { get; set; }
    }
}