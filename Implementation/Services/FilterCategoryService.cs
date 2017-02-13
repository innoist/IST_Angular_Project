using System.Collections.Generic;
using IST.Interfaces.IServices;
using IST.Interfaces.Repository;
using IST.Models.DomainModels;
using IST.Repository.BaseRepository;

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
            return filterCategoryRepository.GetAllFilterCategories();
        }

        public FilterCategory FindFilterCategoryById(int filterCategoryId)
        {
            return filterCategoryRepository.FindFilterCategoryById(filterCategoryId);
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

        public bool DeleteFilterCategory(bool deletetype, int id)
        {
            BaseDbContext context = new BaseDbContext();
            context.DeleteFilterCategory(deletetype, id);
            return true;
        }

        #endregion
    }
}
