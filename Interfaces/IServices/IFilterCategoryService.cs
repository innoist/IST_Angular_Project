using System.Collections.Generic;
using IST.Models.DomainModels;

namespace IST.Interfaces.IServices
{
    public interface IFilterCategoryService
    {
        IEnumerable<FilterCategory> GetAll();
        FilterCategory FindFilterCategoryById(int filterCategoryId);
        bool SaveOrUpdateFilterCategory(FilterCategory filterCategory);
        bool RemoveFilterCategory(int filterCategoryId);
        bool DeleteFilterCategory(int filterCategoryId);
    }
}
