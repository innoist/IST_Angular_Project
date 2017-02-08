using System.Linq;
using IST.Models.DomainModels;
using IST.WebApi2.Models;

namespace IST.WebApi2.ModelMappers
{
    public static class FilterCategoryMapper
    {
        public static FilterCategoryModel MapFromServerToClient(this FilterCategory source)
        {
            var toReturn = new FilterCategoryModel
            {
                Id = source.Id,
                DisplayValue = source.DisplayValue,
                Filters = source.Filters.Select(x => x.MapFromServerToClient()).Where(x => x.SolutionCount > 0).ToList()
            };
            return toReturn;
        }
        public static FilterCategory MapFromClientToServer(this FilterCategoryModel source)
        {
            return new FilterCategory
            {
                Id = source.Id,
                DisplayValue = source.DisplayValue,
                RecCreatedById = source.RecCreatedById,
                RecCreatedOn = source.RecCreatedOn,
                RecLastUpdatedById = source.RecLastUpdatedById,
                RecLastUpdatedOn = source.RecLastUpdatedOn
            };
        }
    }
}