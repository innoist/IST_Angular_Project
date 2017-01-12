using IST.Implementation.Identity;
using IST.Implementation.Services;
using IST.Interfaces.IServices;
using IST.Models.IdentityModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;

namespace IST.Implementation
{
    /// <summary>
    /// Type Registration for Implemention 
    /// </summary>
    public static class TypeRegistrations
    {
        /// <summary>
        /// Register Types for Implementation
        /// </summary>
        public static void RegisterType(IUnityContainer unityContainer)
        {
            UnityConfig.UnityContainer = unityContainer;
            Repository.TypeRegistrations.RegisterType(unityContainer);
            unityContainer.RegisterType<IMenuRightsService, MenuRightsService>();
            unityContainer.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            unityContainer.RegisterType<IWebApiAuthenticationService, WebApiAuthenticationService>();
            unityContainer.RegisterType<IRegisterUserService, RegisterUserService>();
            unityContainer.RegisterType<IClaimsSecurityService, ClaimsSecurityService>();

            unityContainer.RegisterType<IUsersService, UsersService>();

            unityContainer.RegisterType<ISolutionService, SolutionService>();
            unityContainer.RegisterType<ISolutionOwnerService, SolutionOwnerService>();
            unityContainer.RegisterType<ISolutionTypeService, SolutionTypeService>();
            unityContainer.RegisterType<ITagService, TagService>();
            unityContainer.RegisterType<IFilterService, FilterService>();
            unityContainer.RegisterType<IFilterCategoryService, FilterCategoryService>();

        }
    }
}