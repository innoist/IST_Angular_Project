using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using IST.Interfaces.IServices;
using Microsoft.Practices.Unity;

namespace IST.WebApi2.CustomAttributes
{
    /// <summary>
    /// Site Authorize Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class SiteAuthorizeAttribute : AuthorizationFilterAttribute
    {
        /// <summary>
        /// Check if user is authorized on a given permissionKey
        /// </summary>
        private bool IsAuthorized()
        {
            var menuRightsService = UnityConfig.Container.Resolve<IMenuRightsService>();
            var userid = menuRightsService.FindUserId();
            var userPermisions = TCache<List<string>>.Get(userid, 300, () =>
                    menuRightsService.GetUserPermissions(userid).ToList());

            if (userPermisions != null)
            {
                List<string> userPermissionsSet = userPermisions;
                if (userPermissionsSet.Contains(PermissionKey))
                {
                    return true;
                }
                throw new Exception("401");
            }
            return false;
        }
        /// <summary>
        /// Perform the authorization
        /// </summary>
        public override void OnAuthorization(HttpActionContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }
            IsAuthorized();
        }

        public string PermissionKey { get; set; }

    }
}