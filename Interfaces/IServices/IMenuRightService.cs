using System.Collections.Generic;
using System.Linq;
using IST.Models.IdentityModels;
using IST.Models.MenuModels;

namespace IST.Interfaces.IServices
{
    /// <summary>
    /// Interface for Menu Rights Service
    /// </summary>
    public interface IMenuRightsService
    {
        /// <summary>
        /// Find Menu item by Role
        /// </summary>        
        IQueryable<MenuRight> FindMenuItemsByRoleId(string roleId);

        /// <summary>
        /// Save Roles Menu Rights
        /// </summary>
        UserMenuResponse SaveRoleMenuRight(string roleId, string menuIds, UserRole role);

        /// <summary>
        /// Get Role Menu Rights
        /// </summary>
        UserMenuResponse GetRoleMenuRights(string roleId);

        /// <summary>
        /// Returns a complete menu for client side
        /// </summary>
        IEnumerable<MenuView> GetForRole();

        string FindUserId();
        IEnumerable<string> GetUserPermissions(string userId);
    }
}
