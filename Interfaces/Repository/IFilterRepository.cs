using System.Collections.Generic;
using IST.Models.DomainModels;

namespace IST.Interfaces.Repository
{
    public interface IFilterRepository : IBaseRepository<Filter, long>
    {
        IEnumerable<Filter> GetByFilterIds(int[] ids);
        IEnumerable<Filter> GetAllFilters();
        Filter FindFilterById(int filterId);
    }
}
