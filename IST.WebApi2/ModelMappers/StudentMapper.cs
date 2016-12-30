using IST.Commons.Utility;
using IST.Models.DomainModels;
using IST.WebApi2.Models;

namespace IST.WebApi2.ModelMappers
{
    public static class StudentMapper
    {
        public static StudentModel CreateFrom(this Student source)
        {
            return new StudentModel
            {
                StudentId = source.StudentId,
                IsActive = source.IsActive,
                IsDeleted = source.IsDeleted,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedById = source.RecCreatedById,
                RecCreatedOn = source.RecCreatedOn,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedById = source.RecLastUpdatedById,
                RecLastUpdatedOn = source.RecLastUpdatedOn,
                DateOfBirth = source.DateOfBirth,
                DateOfBirthString = source.DateOfBirth.FormatDate(),
                FirstName = source.FirstName,
                AddonsDescription = source.AddonsDescription,
                GuardianEmail = source.GuardianEmail,
                GuardianFirstName = source.GuardianFirstName,
                GuardianPhone = source.GuardianPhone,
                GuardianLastName = source.GuardianLastName,
                HilalOnly = source.HilalOnly,
                Image = source.Image,
                LastName = source.LastName,
                MiddleName = source.MiddleName,
                Age = source.DateOfBirth.ToAgeString(),
            };
        }
    }
}
