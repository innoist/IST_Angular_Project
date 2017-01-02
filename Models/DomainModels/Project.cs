using System.ComponentModel.DataAnnotations.Schema;

namespace IST.Models.DomainModels
{
    public class Project : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
