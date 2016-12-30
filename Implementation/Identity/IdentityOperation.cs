using System.Configuration;
using System.Threading.Tasks;
using IST.Models.IdentityModels;
using IST.Repository.BaseRepository;
using IST.Repository.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Practices.Unity;

namespace IST.Implementation.Identity
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public static class UnityConfig
    {
        private static IUnityContainer unityContainer;
        public static IUnityContainer UnityContainer { get { return unityContainer; } set { unityContainer = value; } }
    }

    /// <summary>
    /// Role Manager
    /// </summary>
    public class ApplicationRoleManager : RoleManager<UserRole, string>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicationRoleManager(IRoleStore<UserRole, string> roleStore)
            : base(roleStore)
        {
        }

        /// <summary>
        /// Create Role
        /// </summary>
        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["BaseDbContext"].ConnectionString;
            
            BaseDbContext db = (BaseDbContext)UnityConfig.UnityContainer.Resolve(typeof(BaseDbContext),
                new ResolverOverride[] { new ParameterOverride("connectionString", connectionString) });

            return new ApplicationRoleManager(new UserRoleStore(db));
        }
    }

    /// <summary>
    /// Email Service
    /// </summary>
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    /// <summary>
    /// Sms Service
    /// </summary>
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your sms service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // This is useful if you do not want to tear down the database each time you run the application.
    // public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    // This example shows you how to create a new database if the Model changes
    //public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    //{
    //    protected override void Seed(ApplicationDbContext context)
    //    {
    //        InitializeIdentityForEF(context);
    //        base.Seed(context);
    //    }

    //    //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
    //    public static void InitializeIdentityForEF(ApplicationDbContext db)
    //    {
    //        var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
    //        var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
    //        const string name = "ali@innostark.com";
    //        const string password = "123456@";
    //        const string roleName = "Admin";

    //        //Create Role Admin if it does not exist
    //        var role = roleManager.FindByName(roleName);
    //        if (role == null)
    //        {
    //            role = new IdentityRole(roleName);
    //            var roleresult = roleManager.Create(role);
    //        }

    //        var user = userManager.FindByName(name);
    //        if (user == null)
    //        {
    //            user = new ApplicationUser { UserName = name, Email = name };
    //            var result = userManager.Create(user, password);
    //            result = userManager.SetLockoutEnabled(user.Id, false);
    //        }

    //        // Add user admin to Role Admin if not already added
    //        var rolesForUser = userManager.GetRoles(user.Id);
    //        if (!rolesForUser.Contains(role.Name))
    //        {
    //            var result = userManager.AddToRole(user.Id, role.Name);
    //        }
    //    }
    //}

    //public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    //{
    //    public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) :
    //        base(userManager, authenticationManager) { }

    //    public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
    //    {
    //        return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
    //    }

    //    public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
    //    {
    //        return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
    //    }
    //}
}
