using System;
using IST.Models.DomainModels;
namespace IST.Models.ModelCopier
{
    public static class StudentCopier
    {
        public static void CopyFrom(this Student target, Student source)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (target == null) throw new ArgumentNullException("target");

                target.StudentId = source.StudentId;
            target.IsActive = source.IsActive;
            target.IsDeleted = source.IsDeleted;
            target.RecCreatedBy = source.RecCreatedBy;
            target.RecCreatedById = source.RecCreatedById;
            target.RecCreatedOn = source.RecCreatedOn;
            target.RecLastUpdatedBy = source.RecLastUpdatedBy;
            target.RecLastUpdatedById = source.RecLastUpdatedById;
            target.RecLastUpdatedOn = source.RecLastUpdatedOn;
            target.DateOfBirth = source.DateOfBirth;
            target.FirstName = source.FirstName;
            target.GuardianEmail = source.GuardianEmail;
            target.GuardianFirstName = source.GuardianFirstName;
            target.GuardianPhone = source.GuardianPhone;
            target.GuardianLastName = source.FirstName;
            target.HilalOnly = source.HilalOnly;
            target.Image = source.Image;
            target.LastName = source.LastName;
            target.MiddleName = source.MiddleName;
        }
    }
}
