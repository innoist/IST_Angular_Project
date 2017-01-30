using System.Linq;
using System.Security.Claims;
using IST.Models.DomainModels;
using IST.WebApi2.Models;
using Microsoft.AspNet.Identity;

namespace IST.WebApi2.ModelMappers
{
    public static class SolutionMapper
    {
        public static SolutionModel MapFromServerToClient(this Solution source)
        {
            return new SolutionModel
            {
                Id = source.Id,
                Description = source.Description,
                Name = source.Name,
                Location = source.Location,
                MaintentanceHours = source.MaintentanceHours,
                OwnerId = source.OwnerId,
                Owner = source.SolutionOwner.DisplayValue,
                Type = source.SolutionType.DisplayValue,
                TypeId = source.TypeId,
                SecurityInfo = source.SecurityInfo,
                Image = source.Image,
                Active = source.Active.HasValue ? source.Active : true,
                TagIds = source.Tags?.Select(x => x.Id).ToList(),
                FilterIds = source.Filters?.Select(x => x.Id).ToList(),
                Tags = source.Tags?.Select(x => x.MapFromServerToClient()).ToList(),
                Filters = source.Filters?.Select(x => x.MapFromServerToClient()).ToList()
            };
        }
        public static SolutionModel ClientCreateFrom(this Solution source)
        {
            var toReturn = new SolutionModel
            {
                Id = source.Id,
                Description = source.Description,
                Name = source.Name,
                Location = source.Location,
                Image = source.Image,
                Active = source.Active.HasValue ? source.Active : true,
                IsFavorite = source.AspNetUsers.FirstOrDefault() != null,
                AverageRating = source.SolutionRatings.Count > 0 ? source.SolutionRatings.Select(x => x.Rating).Average() : 0,
                IsRated = source.SolutionRatings.Count > 0 && source.SolutionRatings.All(x => x.RecCreatedById == ClaimsPrincipal.Current.Identity.GetUserId())
            };
            return toReturn;
        }
        public static Solution MapFromClientToServer(this SolutionModel source)
        {
            return new Solution
            {
                Id = source.Id,
                Description = source.Description,
                Name = source.Name,
                Location = source.Location,
                MaintentanceHours = source.MaintentanceHours,
                OwnerId = source.OwnerId,
                TypeId = source.TypeId,
                SecurityInfo = source.SecurityInfo,
                Image = source.Image,
                Active = source.Active,
                RecCreatedById = source.RecCreatedById,
                RecCreatedOn = source.RecCreatedOn,
                RecLastUpdatedById = source.RecLastUpdatedById,
                RecLastUpdatedOn = source.RecLastUpdatedOn
                //Tags = source.Tags.Select(x => x.MapFromClientToServer()).ToList(),
                //Filters = source.Filters.Select(x => x.MapFromClientToServer()).ToList()
            };
        }
    }
}
