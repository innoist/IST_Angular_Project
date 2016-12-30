using IST.Models.DomainModels;
using IST.WebApi2.Models;

namespace IST.WebApi2.ModelMappers
{
    public static class AllergyMapper
    {
        public static AllergyModel MapFromServerToClient(this Allergy source)
        {
            return new AllergyModel
            {
                AllergyId = source.AllergyId,
                IsImportant = source.IsImportant,
                Description = source.Description,
                IsActive = source.IsActive,
                IsDeleted = source.IsDeleted,
                Title = source.Title,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedById = source.RecCreatedById,
                RecCreatedOn = source.RecCreatedOn,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedById = source.RecLastUpdatedById,
                RecLastUpdatedOn = source.RecLastUpdatedOn,
                IngredientCount = source.IngredientCount
            };
        }
        public static Allergy MapFromClientToServer(this AllergyModel source)
        {
            return new Allergy
            {
                AllergyId = source.AllergyId,
                IsImportant = source.IsImportant,
                Description = source.Description,
                IsActive = source.IsActive,
                IsDeleted = source.IsDeleted,
                Title = source.Title,
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
