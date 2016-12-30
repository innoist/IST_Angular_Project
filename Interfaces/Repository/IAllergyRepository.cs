using System.Collections.Generic;
using System.Threading.Tasks;
using IST.Models.DomainModels;

namespace IST.Interfaces.Repository
{
    /// <summary>
    /// Allergy Repository
    /// </summary>
    public interface IAllergyRepository : IBaseRepository<Allergy, long>
    {
        Allergy GetAllergyByTitle(string title);
        IEnumerable<Allergy> GetAllAllergies();
        IEnumerable<Allergy> GetByAllergyIds(int[] ids);

    }
}
