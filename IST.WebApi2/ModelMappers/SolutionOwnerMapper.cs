using IST.Models.DomainModels;
using IST.WebApi2.Models;

namespace IST.WebApi2.ModelMappers
{
    public static class SolutionOwnerMapper
    {
        public static SolutionOwnerModel MapFromServerToClient(this SolutionOwner source)
        {
            return new SolutionOwnerModel
            {
                Id = source.Id,
                DisplayValue = source.DisplayValue,
            };
        }

        public static SolutionOwner MapFromClientToServer(this SolutionOwnerModel source)
        {
            return new SolutionOwner
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