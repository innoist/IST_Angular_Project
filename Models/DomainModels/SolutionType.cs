using System.Collections.Generic;

namespace IST.Models.DomainModels
{
    public class SolutionType : BaseModel
    {
        public int Id { get; set; }
        public string DisplayValue { get; set; }
        public virtual ICollection<Solution> Solutions { get; set; }
    }
}
