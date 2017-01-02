using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IST.WebApi2.Models
{
    public class ProjectModel : BaseDayCareGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ProjectListView
    {
        public ProjectListView()
        {
            Data = new List<ProjectModel>();
        }
        public IEnumerable<ProjectModel> Data { get; set; }
        public int TotalCount { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
    }
}
