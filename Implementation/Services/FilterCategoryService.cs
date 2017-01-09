using System.Collections.Generic;
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

        public bool SaveFilterCategory(FilterCategory filterCategory)
        {
            filterCategoryRepository.Add(filterCategory);
            filterCategoryRepository.SaveChanges();
            return true;
        }

        public bool UpdateFilterCategory(FilterCategory filterCategory)
        {
            filterCategoryRepository.Update(filterCategory);
            filterCategoryRepository.SaveChanges();
            return true;
        }

        public bool DeleteFilterCategory(int filterCategoryId)
        {
            var toDelete = filterCategoryRepository.Find(filterCategoryId);
            filterCategoryRepository.Delete(toDelete);
            filterCategoryRepository.SaveChanges();
            return true;
        }

        #endregion
    }
}
