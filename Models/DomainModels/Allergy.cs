using System.ComponentModel.DataAnnotations.Schema;

namespace IST.Models.DomainModels
{
    public class Allergy : BaseModel
    {
        public int AllergyId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsImportant { get; set; }
        [NotMapped]
        public int IngredientCount { get; set; }
    }
}
