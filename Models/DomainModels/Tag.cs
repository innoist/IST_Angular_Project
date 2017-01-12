using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IST.Models.DomainModels
{
    /// <summary>
    /// Tag Class
    /// </summary>
    public class Tag : BaseModel
    {
        public int Id { get; set; }
        public string DisplayValue { get; set; }
        public int TagGroupId { get; set; }
        public virtual TagGroup TagGroup { get; set; }
        public virtual ICollection<Solution> Solutions { get; set; }
    }
}
