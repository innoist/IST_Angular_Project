using System;
using System.Configuration;
using System.Threading.Tasks;
using IST.Implementation.Identity;
using IST.Models.IdentityModels;
using IST.Repository.BaseRepository;
using IST.Repository.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using IST.WebApi2.Models;
using Microsoft.Practices.Unity;
using ApplicationUser = IST.WebApi2.Models.ApplicationUser;

namespace IST.WebApi2
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<AspNetUser, string>
    {
        public ApplicationUserManager(IUserStore<AspNetUser, string> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
           IOwinContext context)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BaseDbContext"].ConnectionString;


            BaseDbContext db = (BaseDbContext)UnityConfig.Container.Resolve(typeof(BaseDbContext),
                new ResolverOverride[] { new ParameterOverride("connectionString", connectionString) });

            var manager = new ApplicationUserManager(new UserStore(db));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<AspNetUser, string>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;
            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug in here.
            manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<AspNetUser, string>
            {
                MessageFormat = "Your security code is: {0}"
            });
            manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<AspNetUser, string>
            {
                Subject = "SecurityCode",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<AspNetUser, string>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}
