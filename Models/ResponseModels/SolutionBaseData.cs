using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IST.Models.Common.DropDown;
using IST.Models.DomainModels;

namespace IST.Models.ResponseModels
{
    public class SolutionBaseData
    {

        public Solution Solution { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public IEnumerable<DropDownModel> SolutionOwners { get; set; }
        public IEnumerable<DropDownModel> SolutionTypes { get; set; }
        public IEnumerable<Filter> Filters { get; set; }
    }
}
