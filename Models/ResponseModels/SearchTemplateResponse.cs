using System.Collections.Generic;
using IST.Models.Common.DropDown;

namespace IST.Models.ResponseModels
{
    public class SearchTemplateResponse<T>
    {
        
        public SearchTemplateResponse()
        {
            Data = new List<T>();
        }

        public IEnumerable<T> Data { get; set; }

        public IEnumerable<DropDownModel> DropDown { get; set; } 
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
    }
}
