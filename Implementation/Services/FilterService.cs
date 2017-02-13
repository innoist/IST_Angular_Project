using System.Collections.Generic;
using IST.Interfaces.IServices;
using IST.Interfaces.Repository;
using IST.Models.DomainModels;
using IST.Repository.BaseRepository;

namespace IST.Implementation.Services
{
    public class FilterService : IFilterService
    {
        #region Private

        private readonly IFilterRepository filterRepository;

        #endregion

        #region Constructor

        public FilterService(IFilterRepository filterRepository)
        {
            this.filterRepository = filterRepository;
        }

        #endregion

        #region Public

        public IEnumerable<Filter> GetAll()
        {
            return filterRepository.GetAllFilters();
        }

        public Filter FindFilterById(int filterId)
        {
            return filterRepository.FindFilterById(filterId);
        }
        
        public bool SaveOrUpdateFilter(Filter filter)
        {
            if (filter.Id > 0)
            {
                var filterToUpdate = filterRepository.Find(filter.Id);
                filterToUpdate.DisplayValue = filter.DisplayValue;
                filterToUpdate.FilterCategoryId = filter.FilterCategoryId;
                filterToUpdate.RecLastUpdatedById = filter.RecLastUpdatedById;
                filterToUpdate.RecLastUpdatedOn = filter.RecLastUpdatedOn;
                filterRepository.Update(filterToUpdate);
            }
            else
            {
                filterRepository.Add(filter);
            }
            filterRepository.SaveChanges();
            return true;
        }

        public bool DeleteFilter(bool deletetype, int filterId)
        {
            BaseDbContext context = new BaseDbContext();
            context.DeleteFilter(deletetype, filterId);
            return true;
        }
        
        #endregion
    }
}
