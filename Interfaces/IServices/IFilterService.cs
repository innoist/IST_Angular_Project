using System.Collections.Generic;
using IST.Models.DomainModels;

namespace IST.Interfaces.IServices
{
    public interface IFilterService
    {
        IEnumerable<Filter> GetAll();
        Filter FindFilterById(int filterId);
        bool SaveOrUpdateFilter(Filter filter);
        bool RemoveFilter(int id);
        bool DeleteFilter(int id);
    }
}
