using IST.Models.DomainModels;
using IST.WebApi2.Models;

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
                MaintentanceHours=source.MaintentanceHours,
                OwnerId=source.OwnerId,
                TypeId=source.TypeId,
                SecurityInfo = source.SecurityInfo,
                Image = source.Image,
                Active = source.Active
            };
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
                SecurityInfo=source.SecurityInfo,
                Image=source.Image,
                Active=source.Active
            };
        }
    }
}
