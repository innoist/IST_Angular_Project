using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using IST.Interfaces.IServices;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;

namespace IST.WebApi2.Custom_Attributes
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
        /// <summary>
        /// Redirects request to unauthroized request page
        /// </summary>
        //protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        //{
        //    if (filterContext.HttpContext.User == null || !filterContext.HttpContext.User.Identity.IsAuthenticated)
        //    {
        //        base.HandleUnauthorizedRequest(filterContext);
        //    }
        //    else
        //    {
        //        //filterContext.Result =
        //        //    new RedirectToRouteResult(
        //        //        new RouteValueDictionary(
        //        //            new { area = "", controller = "UnauthorizedRequest", action = "Index" }));
        //        filterContext.Result = new HttpUnauthorizedResult("401 : You are Unauthorized for this action.");
        //    }
        //}
        /// <summary>
        /// Permission Key attribute to be set on caller method
        /// </summary>
        public string PermissionKey { get; set; }

    }
}