using System.Collections.Generic;
using IST.Models.DomainModels;

namespace IST.Interfaces.IServices
{
    /// <summary>
    /// Interface for Allergy Service
    /// </summary>
    public interface IAllergyService
    {
        IEnumerable<Allergy> GetAllergies();
        Allergy GetAllergyById(int id);
        bool SaveOrUpdate(Allergy allergy);
        bool Delete(int id);
        IEnumerable<Allergy> GetByAllergyIds(int[] ids);

    }
}
