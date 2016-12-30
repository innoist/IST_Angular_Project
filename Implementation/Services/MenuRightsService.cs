using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using IST.Commons;
using IST.Interfaces.IServices;
using IST.Interfaces.Repository;
using IST.Models.IdentityModels;
using IST.Models.MenuModels;

namespace IST.Implementation.Services
{
    /// <summary>
    /// Menu Rights Service
    /// </summary>
    public sealed class MenuRightsService : IMenuRightsService
    {
        #region Private
        private readonly IMenuRightRepository menuRightRepository;
        private readonly IMenuRepository menuRepository;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MenuRightsService(IMenuRightRepository menuRightRepository, IMenuRepository menuRepository)
        {
            if (menuRightRepository == null)
            {
                throw new ArgumentNullException("menuRightRepository");
            }
            if (menuRepository == null)
            {
                throw new ArgumentNullException("menuRepository");
            }

            this.menuRightRepository = menuRightRepository;
            this.menuRepository = menuRepository;
        }

        #endregion

        #region Public
        /// <summary>
        /// Find Menu Items by Role ID
        /// </summary>
        public IQueryable<MenuRight> FindMenuItemsByRoleId(string roleId)
        {
            return menuRightRepository.GetMenuByRole(roleId);
        }

        /// <summary>
        /// Save Menu Rights
        /// </summary>
        public UserMenuResponse SaveRoleMenuRight(string roleId, string menuIds, UserRole role)
        {
            List<UserRole> roles = menuRepository.Roles().OrderBy(dbRole => dbRole.Name).ToList();
            List<Menu> menues = menuRepository.GetAll().ToList();
            IList<string> postedMenuIdstrings = menuIds.Split(',');
            IList<int> postedMenuIds = new List<int>();
            if (postedMenuIdstrings.Count > 0 && !string.IsNullOrEmpty(postedMenuIdstrings[0]))
                postedMenuIds = postedMenuIdstrings.Select(int.Parse).ToList();
            List<MenuRight> userMenuRights = menuRightRepository.GetMenuByRole(roleId).ToList();

            foreach (int menuItem in postedMenuIds)
            {
                if (userMenuRights.All(right => right.Menu.MenuId != menuItem))
                {
                    MenuRight toBeAddedMenu = new MenuRight
                    {
                        Menu = menues.FirstOrDefault(dbMenu => dbMenu.MenuId == menuItem),
                        UserRole = roles.FirstOrDefault(dbRole => dbRole.Id == roleId)
                    };
                    menuRightRepository.Add(toBeAddedMenu);
                }
            }

            IEnumerable<MenuRight> deleted = userMenuRights.Where(menu => !postedMenuIds.Contains(menu.Menu.MenuId));
            deleted.ToList().ForEach(menu => menuRightRepository.Delete(menu));
            menuRightRepository.SaveChanges();

            return new UserMenuResponse
            {
                MenuRights = FindMenuItemsByRoleId(roleId).ToList(),
                Menus = menuRepository.GetAll().ToList(),
                Roles = roles
            };
        }

        /// <summary>
        /// Get Menu Rights For Role
        /// </summary>
        public UserMenuResponse GetRoleMenuRights(string roleId)
        {
            List<UserRole> roles = menuRepository.Roles().OrderBy(role => role.Name).ToList();
            return new UserMenuResponse
            {
                Roles = roles,
                MenuRights = FindMenuItemsByRoleId(string.IsNullOrEmpty(roleId) && roles.Count > 0 ? roles[0].Id : roleId).ToList(),
                Menus = menuRepository.GetAll().ToList(),
            };
        }

        /// <summary>
        /// Returns a complete menu for client side
        /// </summary>
        public IEnumerable<MenuView> GetForRole()
        {
            Claim userRoleClaim = ClaimHelper.GetClaimToString(ClaimTypes.Role);
            if (userRoleClaim == null || string.IsNullOrEmpty(userRoleClaim.Value))
            {
                return null;
            }

            IEnumerable<MenuRight> menuRights = menuRightRepository.GetByRoleName(userRoleClaim.Value).ToList();
            // Get Parent Items 
            IEnumerable<Menu> parents = menuRights.Where(menu => menu.Menu.IsRootItem).OrderBy(menu => menu.Menu.SortOrder).Select(menu => menu.Menu).ToList();

            List<MenuView> menuViews = new List<MenuView>();
            foreach (Menu parent in parents)
            {
                MenuView menuView = new MenuView
                {
                    text = parent.MenuTitle,
                    heading = true,
                    icon = parent.MenuImagePath,
                    sref = parent.MenuTargetController
                };

                menuViews.Add(menuView);

                List<Menu> NotParentMenu = menuRights
                                    .Where(menu => !menu.Menu.IsRootItem && menu.Menu.ParentItem_MenuId.Equals(parent.MenuId))
                                    .OrderBy(menu => menu.Menu.SortOrder).Select(menu => menu.Menu).ToList();

                foreach (var menus in NotParentMenu)
                {
                    MenuView menuViewz = new MenuView
                    {
                        text = menus.MenuTitle,
                        icon = menus.MenuImagePath,
                        sref = menus.MenuTargetController,
                        submenu = new List<MenuView>()
                    };

                    menuViews.Add(menuViewz);

                    // Insert Sub menus if any
                    List<Menu> childs = menuRights
                                        .Where(menu => !menu.Menu.IsRootItem && menu.Menu.ParentItem_MenuId == menus.MenuId)
                                        .OrderBy(menu => menu.Menu.SortOrder).Select(menu => menu.Menu).ToList();

                    if (!childs.Any())
                    {
                        continue;
                    }

                    childs.ForEach(childMenu => menuViewz.submenu.Add(new MenuView
                    {
                        text = childMenu.MenuTitle,
                        icon = childMenu.MenuImagePath,
                        sref = childMenu.MenuTargetController
                    }));
                }
            }
            return menuViews;
        }

        #endregion
    }
}
