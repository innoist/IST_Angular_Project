using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IST.Models.DomainModels;
using IST.WebApi2.Models;

namespace IST.WebApi2.ModelMappers
{
    public static class SolutionTypeMapper
    {
        public static SolutionTypeModel MapFromServerToClient(this SolutionType source)
        {
            return new SolutionTypeModel
            {
                Id = source.Id,
                DisplayValue = source.DisplayValue
            };
        }
        public static SolutionType MapFromClientToServer(this SolutionTypeModel source)
        {
            return new SolutionType
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