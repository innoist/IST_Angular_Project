using System;
using System.Collections.Generic;
using IST.Interfaces.IServices;
using IST.Interfaces.Repository;
using IST.Models.DomainModels;

namespace IST.Implementation.Services
{
    /// <summary>
    /// Menu Rights Service
    /// </summary>
    public sealed class AllergyService : IAllergyService
    {
        #region Private
        private readonly IAllergyRepository allergyRepository;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public AllergyService(IAllergyRepository allergyRepository)
        {
            if (allergyRepository == null)
            {
                throw new ArgumentNullException(nameof(allergyRepository));
            }
            
            this.allergyRepository = allergyRepository;
        }

        #endregion

        #region Public
        /// <summary>
        /// Get All Allergies
        /// </summary>
        public IEnumerable<Allergy> GetAllergies()
        {
            return allergyRepository.GetAllAllergies();
        }

        /// <summary>
        /// Get All Allergies with detail like ingredient count
        /// </summary>

        /// <summary>
        /// Get Allergy By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Allergy GetAllergyById(int id)
        {
            return allergyRepository.Find(id);
        }

        public bool SaveOrUpdate(Allergy allergy)
        {
            var oldItem = allergyRepository.GetAllergyByTitle(allergy.Title);
            if (allergy.AllergyId > 0)
            {
                if (oldItem != null && oldItem.AllergyId != allergy.AllergyId)
                    return false;
                allergyRepository.Update(allergy);
            }
            else
            {
                if (oldItem != null)
                    return false;
                allergyRepository.Add(allergy);
            }

            allergyRepository.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var toDelete = allergyRepository.Find(id);
            if (toDelete == null)
            {
                throw new Exception("Ingredient Category may have already deleted.");
            }
            
            toDelete.IsDeleted = true;
            allergyRepository.SaveChanges();
            return true;
        }

        

        public IEnumerable<Allergy> GetByAllergyIds(int[] ingredientIds)
        {
            return allergyRepository.GetByAllergyIds(ingredientIds);
        }
        #endregion


    }
}
