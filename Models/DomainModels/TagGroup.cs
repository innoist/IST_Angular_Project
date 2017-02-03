using System.Collections.Generic;

namespace IST.Models.DomainModels
{
    public class TagGroup : BaseModel
    {
        public int Id { get; set; }
        public string DisplayValue { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
