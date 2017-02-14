// Copyright (c) KriaSoft, LLC.  All rights reserved.
// Licensed under the Apache License, Version 2.0.  See LICENSE.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using IST.Models.IdentityModels;
using IST.Repository.BaseRepository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Practices.Unity;
using SendGrid.SmtpApi;
using IST.Repository.Repositories;
namespace IST.Implementation.Identity
{
    /// <summary>
    /// Application User Manager
    /// </summary>
    public class ApplicationUserManager : UserManager<AspNetUser, string>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicationUserManager(IUserStore<AspNetUser, string> store)
            : base(store)
        {
        }

        /// <summary>
        /// Send Email
        /// </summary>
        public override Task SendEmailAsync(string email, string subject, string body)
        {
             
            string fromAddress = ConfigurationManager.AppSettings["FromAddress"];
            string fromPwd = ConfigurationManager.AppSettings["FromPassword"];
            string fromDisplayName = ConfigurationManager.AppSettings["FromDisplayName"];
            //string cc = ConfigurationManager.AppSettings["CC"];
            //string bcc = ConfigurationManager.AppSettings["BCC"];

            //Getting the file from config, to send
            MailMessage oEmail = new MailMessage
            {
                From = new MailAddress(fromAddress, fromDisplayName),
                Subject = subject,
                IsBodyHtml = true,
                Body = body,
                Priority = MailPriority.High
            };
            oEmail.To.Add(email);
            string smtpServer = ConfigurationManager.AppSettings["SMTPServer"];
            string smtpPort = ConfigurationManager.AppSettings["SMTPPort"];
            string enableSsl = ConfigurationManager.AppSettings["EnableSSL"];
            SmtpClient client = new SmtpClient(smtpServer, Convert.ToInt32(smtpPort))
            {
                EnableSsl = enableSsl == "1",
                Credentials = new NetworkCredential(fromAddress, fromPwd)
            };

            return client.SendMailAsync(oEmail);

        }

        string XsmtpapiHeaderAsJson()
        {
            var header = new Header();

            var uniqueArgs = new Dictionary<string, string> {
				{
					"cares",
					"invitation"
				},				
			};
            header.AddUniqueArgs(uniqueArgs);

            return header.JsonString();
        }
        public void SendEmailSendGrid(string emailTo, string subject, string body)
        {
            SmtpClient client = new SmtpClient();
            client.Port = int.Parse(ConfigurationManager.AppSettings["SendGridSmtpPort"]);
            client.Host = ConfigurationManager.AppSettings["SendGridSmtpServer"];
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(ConfigurationManager.AppSettings["SendGridFromEmail"], ConfigurationManager.AppSettings["SendGridFromName"]);
            mail.Subject = subject;
            mail.Body = body;
            client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SendGridUserName"], ConfigurationManager.AppSettings["SendGridPassword"]);
            mail.IsBodyHtml = true;
            string xmstpapiJson = XsmtpapiHeaderAsJson();
            mail.To.Add(new MailAddress(emailTo));

            mail.Headers.Add("X-SMTPAPI", xmstpapiJson);
            //client.SendAsync(mail, null);
            client.SendCompleted += (sender, error) =>
            {
                if (error.Error != null)
                {
                    throw new WarningException(string.Format("Email Failed to send. Email Invite to : {0}", mail.To));
                }
                client.Dispose();
                mail.Dispose();
            };
            ThreadPool.QueueUserWorkItem(o => client.SendAsync(mail, Tuple.Create(client, mail)));
        
            
        }

        /// <summary>
        /// Create User
        /// </summary>
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["BaseDbContext"].ConnectionString;


            BaseDbContext db = (BaseDbContext)UnityConfig.UnityContainer.Resolve(typeof(BaseDbContext), 
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