using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IST.WebApi2.Models
{
    public class SolutionModel : BaseDayCareGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaintentanceHours { get; set; }
        public int OwnerId { get; set; }
        public int TypeId { get; set; }
        public string Location { get; set; }
        public string SecurityInfo { get; set; }
        public string Image { get; set; }
        public bool Active { get; set; }
    }

    public class ProjectListView
    {
        public ProjectListView()
        {
            Data = new List<SolutionModel>();
        }
        public IEnumerable<SolutionModel> Data { get; set; }
        public int TotalCount { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
    }
}
