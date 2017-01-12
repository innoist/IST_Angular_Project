using System.Collections.Generic;
using IST.Models.DomainModels;

namespace IST.Models.ResponseModels
{
    public class SolutionCreateResponseModel
    {
        public Solution Solution { get; set; }
        public List<int> TagIds { get; set; }
        public List<int> FilterIds { get; set; }
    }
}
