using System.Collections.Generic;
using IST.Models.DomainModels;

namespace IST.Interfaces.Repository
{
    public interface IFilterCategoryRepository : IBaseRepository<FilterCategory, long>
    {
        IEnumerable<FilterCategory> GetAllFilterCategories();
        FilterCategory FindFilterCategoryById(int filterCategoryId);
    }
}
