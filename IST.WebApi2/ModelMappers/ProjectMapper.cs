using IST.Models.DomainModels;
using IST.WebApi2.Models;

namespace IST.WebApi2.ModelMappers
{
    public static class ProjectMapper
    {
        public static ProjectModel MapFromServerToClient(this Project source)
        {
            return new ProjectModel
            {
                Id = source.Id,
                Description = source.Description,
                IsActive = source.IsActive,
                IsDeleted = source.IsDeleted,
                Name = source.Name,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedById = source.RecCreatedById,
                RecCreatedOn = source.RecCreatedOn,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedById = source.RecLastUpdatedById,
                RecLastUpdatedOn = source.RecLastUpdatedOn,
            };
        }
        public static Project MapFromClientToServer(this ProjectModel source)
        {
            return new Project
            {
                Id = source.Id,
                Description = source.Description,
                IsActive = source.IsActive,
                IsDeleted = source.IsDeleted,
                Name = source.Name,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedById = source.RecCreatedById,
                RecCreatedOn = source.RecCreatedOn,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedById = source.RecLastUpdatedById,
                RecLastUpdatedOn = source.RecLastUpdatedOn,
            };
        }
    }
}
