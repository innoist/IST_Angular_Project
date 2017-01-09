using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IST.Models.DomainModels
{
    public class SolutionOwner
    {
        public int Id { get; set; }
        public string DisplayValue { get; set; }
        public virtual ICollection<Solution> Solutions { get; set; }
    }
}
