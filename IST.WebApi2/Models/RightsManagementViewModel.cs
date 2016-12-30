using System.Collections.Generic;
using IST.Models.IdentityModels;
using IST.Models.MenuModels;

namespace IST.WebApi2.Models
{
    /// <summary>
    /// Rights Management
    /// </summary>
    public class RightsManagementViewModel
    {
        public List<MenuRight> Rights { get; set; }
        
        public string SelectedRoleId { get; set; }

        public List<UserRole> Roles { get; set; }
    }
}