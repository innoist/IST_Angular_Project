using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using IST.Interfaces.Repository;
using IST.Models.Common;
using IST.Models.DomainModels;
using IST.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace IST.Repository.Repositories
{
    /// <summary>
    /// Menu Repository
    /// </summary>
    public sealed class AllergyRepository : BaseRepository<Allergy>, IAllergyRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public AllergyRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Allergy> DbSet => db.Allergies;

        #endregion

        #region Public

        public Task<Allergy> GetAllergyById(int id)
        {
            return DbSet.FirstOrDefaultAsync(x => x.AllergyId.Equals(id) && !x.IsDeleted);
        }

        public Allergy GetAllergyByTitle(string title)
        {
            return DbSet.FirstOrDefault(x => x.Title.ToLower().Equals(title.ToLower()) && !x.IsDeleted);
        }

        public IEnumerable<Allergy> GetAllAllergies()
        {
            return db.Allergies.Where(x => !x.IsDeleted).OrderBy(x=>x.Title);
        }

        public IEnumerable<Allergy> GetAllOptionalAllergies()
        {
            return db.Allergies.Where(x => !x.IsDeleted && !x.IsImportant);
        }
        

        private readonly Dictionary<OrderByAllergy, Func<Allergy, object>> orderClause =

            new Dictionary<OrderByAllergy, Func<Allergy, object>>
            {
                {OrderByAllergy.Title, c => c.Title},
                {OrderByAllergy.Id, c => c.AllergyId},
                {OrderByAllergy.Description, c => c.Description}
            };

        public IEnumerable<Allergy> GetByAllergyIds(int[] ids)
        {
            return DbSet.Where(x => !x.IsDeleted && ids.Contains(x.AllergyId));
        }

        #endregion
    }
}
