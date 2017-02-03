using System.Collections.Generic;
using IST.Models.Common.DropDown;

namespace IST.WebApi2.Models
{
    public class TagModel : BaseModel
    {
        public int Id { get; set; }
        public string DisplayValue { get; set; }
        public int TagGroupId { get; set; }
        public string TagGroupName { get; set; }
    }
    public class TagViewModel
    {
        public TagModel TagModel { get; set; }
        public List<DropDownModel> TagGroups { get; set; }
    }
}