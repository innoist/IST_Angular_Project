using System.Linq;
using IST.Models.DomainModels;
using IST.WebApi2.Models;

namespace IST.WebApi2.ModelMappers
{
    public static class FilterMapper
    {
        public static FilterModel MapFromServerToClient(this Filter source)
        {
            return new FilterModel
            {
                Id = source.Id,
                DisplayValue = source.DisplayValue,
                FilterCategoryId = source.FilterCategoryId,
                SolutionCount = source.Solutions.Where(x => x.Active == true).ToList().Count,
                FilterCategoryName = source.FilterCategory.DisplayValue
            };
        }
        public static Filter MapFromClientToServer(this FilterModel source)
        {
            return new Filter
            {
                Id = source.Id,
                DisplayValue = source.DisplayValue,
                FilterCategoryId = source.FilterCategoryId,
                RecCreatedById = source.RecCreatedById,
                RecCreatedOn = source.RecCreatedOn,
                RecLastUpdatedById = source.RecLastUpdatedById,
                RecLastUpdatedOn = source.RecLastUpdatedOn
            };
        }
    }
}