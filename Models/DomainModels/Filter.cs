using System.Collections.Generic;

namespace IST.Models.DomainModels
{
    public class Filter : BaseModel
    {
        public int Id { get; set; }
        public int FilterCategoryId { get; set; }
        public string DisplayValue { get; set; }
        public virtual ICollection<Solution> Solutions { get; set; }
        public virtual FilterCategory FilterCategory { get; set; }
    }
}
