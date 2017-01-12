using System.Collections.Generic;
using IST.Interfaces.IServices;
using IST.Interfaces.Repository;
using IST.Models.DomainModels;

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
            return filterRepository.GetAll();
        }

        public Filter FindFilterById(int filterId)
        {
            return filterRepository.Find(filterId);
        }
        
        public bool SaveFilter(Filter filter)
        {
            filterRepository.Add(filter);
            filterRepository.SaveChanges();
            return true;
        }

        public bool UpdateFilter(Filter filter)
        {
            filterRepository.Update(filter);
            filterRepository.SaveChanges();
            return true;
        }

        public bool DeleteFilter(int filterId)
        {
            var toDelete = filterRepository.Find(filterId);
            filterRepository.Delete(toDelete);
            filterRepository.SaveChanges();
            return true;
        }

        #endregion
    }
}
