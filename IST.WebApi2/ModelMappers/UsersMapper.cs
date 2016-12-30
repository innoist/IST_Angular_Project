using IST.Models.IdentityModels;
using IST.WebApi2.Models;

namespace IST.WebApi2.ModelMappers
{
    public static class UsersMapper
    {
        public static UsersModel MapUserFromServerToClient(this AspNetUser source)
        {
            var toReturn = new UsersModel
            {
                Address = source.Address,
                CompanyName = source.CompanyName,
                Email = source.Email,
                FirstName = source.FirstName,
                Id = source.Id,
                ImageName = source.ImageName,
                LastName = source.LastName,
                Telephone = source.Telephone,
                UserName = source.UserName,
            };
            return toReturn;
        }

        public static RoleDDL MapRoleFromServerToClient(this UserRole source)
        {
            return new RoleDDL
            {
                Id = source.Id,
                Name = source.Name
            };
        }
    }
}