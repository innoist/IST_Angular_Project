using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IST.Models.Common.DropDown;
using IST.Models.DomainModels;

namespace IST.WebApi2.Models
{
    public class SolutionModel : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? MaintentanceHours { get; set; }
        public int OwnerId { get; set; }
        public string Owner { get; set; }
        public int TypeId { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public string SecurityInfo { get; set; }
        public string Image { get; set; }
        public bool? Active { get; set; }
        
        public List<int> TagIds { get; set; }
        public List<int> FilterIds { get; set; }

        public List<TagModel> Tags { get; set; }
        public List<FilterModel> Filters { get; set; }
    }

    public class ProjectListView
    {
        public ProjectListView()
        {
            Data = new List<SolutionModel>();
        }

        public IEnumerable<SolutionModel> Data { get; set; }
        public IEnumerable<SolutionTypeModel> SolutionTypes { get; set; }
        public IEnumerable<DropDownModel> FilterCategories { get; set; }
        public int TotalCount { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
    }

    public class SolutionViewModel
    {
        public SolutionModel SolutionModel { get; set; }
        public IEnumerable<DropDownModel> Tags { get; set; }
        public IEnumerable<DropDownModel> Filters { get; set; }
        public IEnumerable<DropDownModel> SolutionTypes { get; set; }
        public IEnumerable<DropDownModel> SolutionOwners { get; set; }
    }
}
