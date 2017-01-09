using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.Models.DomainModels
{
    public class TagGroup
    {
        public int Id { get; set; }
        public string DisplayValue { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
