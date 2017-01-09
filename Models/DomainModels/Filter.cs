using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.Models.DomainModels
{
    public class Filter
    {
        public int Id { get; set; }
        public int FilterCategoryId { get; set; }
        public string DisplayValue { get; set; }
        public virtual ICollection<Solution> Solutions { get; set; }
        public virtual FilterCategory FilterCategory { get; set; }
    }
}
