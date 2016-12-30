using System.Collections.Generic;
using System.Linq;
using IST.Models.MenuModels;

namespace IST.Interfaces.Repository
{
    public interface IMenuRightRepository : IBaseRepository<MenuRight, long>
    {
        IQueryable<MenuRight> GetMenuByRole(string roleId);

        IEnumerable<MenuRight> GetByRoleName(string role);
    }
}
