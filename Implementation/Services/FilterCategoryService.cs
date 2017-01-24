using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using IST.Interfaces.IServices;
using IST.Interfaces.Repository;
using IST.Models.DomainModels;

namespace IST.Implementation.Services
{
    public class FilterCategoryService : IFilterCategoryService
    {
        #region Private

        private readonly IFilterCategoryRepository filterCategoryRepository;

        #endregion

        #region Constructor

        public FilterCategoryService(IFilterCategoryRepository filterCategoryRepository)
        {
            this.filterCategoryRepository = filterCategoryRepository;
        }

        #endregion

        #region Public

        public IEnumerable<FilterCategory> GetAll()
        {
            return filterCategoryRepository.GetAll();
        }

        public FilterCategory FindFilterCategoryById(int filterCategoryId)
        {
            return filterCategoryRepository.Find(filterCategoryId);
        }

        public bool SaveOrUpdateFilterCategory(FilterCategory filterCategory)
        {
            if (filterCategory.Id > 0)
            {
                var categoryToUpdate = filterCategoryRepository.Find(filterCategory.Id);
                categoryToUpdate.DisplayValue = filterCategory.DisplayValue;
                categoryToUpdate.RecLastUpdatedById = filterCategory.RecLastUpdatedById;
                categoryToUpdate.RecLastUpdatedOn = filterCategory.RecLastUpdatedOn;
                filterCategoryRepository.Update(categoryToUpdate);
            }
            else
            {
                filterCategoryRepository.Add(filterCategory);
            }
            filterCategoryRepository.SaveChanges();
            return true;
        }

        public bool DeleteFilterCategory(int filterCategoryId)
        {
            var toDelete = filterCategoryRepository.Find(filterCategoryId);
            if (toDelete.Filters.Any())
                return false;
            filterCategoryRepository.Delete(toDelete);
            filterCategoryRepository.SaveChanges();
            return true;
        }

        #endregion
    }
}
