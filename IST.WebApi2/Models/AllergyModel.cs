using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IST.WebApi2.Models
{
    public class AllergyModel: BaseDayCareGroup
    {
        public int AllergyId { get; set; }
        [Required(ErrorMessage = nameof(Title) + " is required")]
        [MaxLength(100)]
        public string Title { get; set; }
        public bool IsImportant { get; set; }
        public string Description { get; set; }
        public int IngredientCount { get; set; }
    }

    public class AllergyLV
    {

        public AllergyLV()
        {
            data = new List<AllergyModel>();
        }

        public IEnumerable<AllergyModel> data { get; set; }
        public int TotalCount { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
    }
}
