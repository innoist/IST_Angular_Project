using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using IST.Interfaces.IServices;
using IST.Models.IdentityModels;
using IST.Models.MenuModels;
using IST.Models.RequestModels;
using IST.WebApi2.Models;
using IST.WebBase.Mvc;

namespace IST.WebApi2.Controllers
{
    [Authorize]
    public class RightsManagementController : ApiController
    {
        #region Private

        private readonly IMenuRightsService menuRightsService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public RightsManagementController(IMenuRightsService menuRightsService)
        {
            if (menuRightsService == null)
            {
                throw new ArgumentNullException("menuRightsService");
            }

            this.menuRightsService = menuRightsService;
        }

        #endregion

        #region Public

        [Authorize(Roles = "SystemAdministrator")]
        [ApiException]
        public RightsManagementViewModel Get([FromUri] RightsManagementRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");    
            }

            UserMenuResponse response = menuRightsService.GetRoleMenuRights(request.RoleId);
            RightsManagementViewModel rightsManagementViewModel = new RightsManagementViewModel
            {
                Roles =
                    response.Roles != null ? response.Roles.Select(role => new UserRole { Id = role.Id, Name =  role.Name }).ToList() : new List<UserRole>(),
                Rights = response.Menus.Select(
                    m =>
                    new Models.MenuRight
                    {
                        MenuId = m.MenuId,
                        MenuTitle = m.MenuTitle,
                        IsParent = m.IsRootItem,
                        IsSelected =
                            response.MenuRights.Any(
                                menu =>
                            menu.Menu.MenuId == m.MenuId),
                        ParentId = m.ParentItem_MenuId
                    }).ToList(),
                SelectedRoleId = string.IsNullOrEmpty(request.RoleId) && 
                    (response.Roles != null && response.Roles.Any()) ? response.Roles[0].Id : request.RoleId
            };

            return rightsManagementViewModel;
        }

        /// <summary>
        /// Update Rights
        /// </summary>
        [Authorize(Roles = "SystemAdministrator")]
        [ApiException]
        [ValidateFilter]
        public void Post(RightsManagementRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.RoleId))
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            HttpContext.Current.Session["Menu"] = null;
            menuRightsService.SaveRoleMenuRight(request.RoleId, request.SelectedMenuIds, null);
        }

        #endregion
    }
}
