using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IST.Models.DomainModels;
using IST.WebApi2.Models;

namespace IST.WebApi2.ModelMappers
{
    public static class TagMapper
    {
        public static TagModel MapFromServerToClient(this Tag source)
        {
            return new TagModel
            {
                Id = source.Id,
                DisplayValue = source.DisplayValue,
                TagGroupId=source.TagGroupId,
                TagGroupName=source.TagGroup.DisplayValue
            };
        }
        public static Tag MapFromClientToServer(this TagModel source)
        {
            return new Tag
            {
                Id = source.Id,
                DisplayValue = source.DisplayValue,
                TagGroupId = source.TagGroupId,
                RecCreatedById = source.RecCreatedById,
                RecCreatedOn = source.RecCreatedOn,
                RecLastUpdatedById = source.RecLastUpdatedById,
                RecLastUpdatedOn = source.RecLastUpdatedOn
            };
        }
    }
}