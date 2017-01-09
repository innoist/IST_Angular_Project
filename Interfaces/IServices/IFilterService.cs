using System.Collections.Generic;
using IST.Models.DomainModels;

namespace IST.Interfaces.IServices
{
    public interface IFilterService
    {
        IEnumerable<Filter> GetAll();
        Filter FindFilterById(int filterId);
        bool SaveFilter(Filter filter);
        bool UpdateFilter(Filter filter);
        bool DeleteFilter(int filterId);
    }
}
