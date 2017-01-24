using IST.Models.DomainModels;
using IST.WebApi2.Models;

namespace IST.WebApi2.ModelMappers
{
    public static class TagGroupMapper
    {
        public static TagGroupModel MapFromServerToClient(this TagGroup source)
        {
            return new TagGroupModel
            {
                Id = source.Id,
                DisplayValue = source.DisplayValue
            };
        }
        public static TagGroup MapFromClientToServer(this TagGroupModel source)
        {
            return new TagGroup
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