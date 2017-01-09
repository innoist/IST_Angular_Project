using System.Collections.Generic;
using IST.Models.DomainModels;

namespace IST.Interfaces.IServices
{
    public interface IFilterCategoryService
    {
        IEnumerable<FilterCateogry> GetAll();
        FilterCateogry FindFilterCategoryById(int filterCategoryId);
        bool SaveFilterCategory(FilterCateogry filterCategory);
        bool UpdateFilterCategory(FilterCateogry filterCategory);
        bool DeleteFilterCategory(int filterCategoryId);
    }
}
