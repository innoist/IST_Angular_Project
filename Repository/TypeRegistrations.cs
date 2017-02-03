using IST.Interfaces.Repository;
using IST.Repository.BaseRepository;
using IST.Repository.Repositories;
using Microsoft.Practices.Unity;
namespace IST.Repository
{
    /// <summary>
    /// Repository Type Registration
    /// </summary>
    public static class TypeRegistrations
    {
        /// <summary>
        /// Register Types for Repositories
        /// </summary>
        public static void RegisterType(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<IMenuRightRepository, MenuRightRepository>();
            unityContainer.RegisterType<BaseDbContext>(new PerRequestLifetimeManager());
            unityContainer.RegisterType<IMenuRepository, MenuRepository>();
            unityContainer.RegisterType<IWebApiUserRepository, WebApiUserRepository>();
            unityContainer.RegisterType<IUserRepository, UserRepository>();
            unityContainer.RegisterType<IUserDetailsRepository, UserDetailsRepository>();
         
            unityContainer.RegisterType<ILogRepository, LogRepository>();
           
            unityContainer.RegisterType<IAspNetRoleRepository, AspNetRoleRepository>();
            unityContainer.RegisterType<ISolutionRepository, SolutionRepository>();
            unityContainer.RegisterType<ISolutionOwnerRepository, SolutionOwnerRepository>();
            unityContainer.RegisterType<ISolutionTypeRepository, SolutionTypeRepository>();
            unityContainer.RegisterType<IFilterRepository, FilterRepository>();
            unityContainer.RegisterType<IFilterCategoryRepository, FilterCategoryRepository>();
            unityContainer.RegisterType<ITagRepository, TagRepository>();
            unityContainer.RegisterType<ITagGroupRepository, TagGroupRepository>();
            unityContainer.RegisterType<ISolutionRatingRepository, SolutionRatingRepository>();
            unityContainer.RegisterType<ISolutionSearchHistoryRepository, SolutionSearchHistoryRepository>();
            unityContainer.RegisterType<IUsageHistoryRepository, UsageHistoryRepository>();
        }
    }
}