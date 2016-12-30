using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace IST.WebBase.Mvc
{
    /// <summary>
    /// Site Authorize Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class SiteAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Check if user is authorized on a given permissionKey
        /// </summary>
        private bool IsAuthorized(HttpContextBase httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                httpContext.User = null;
                return false;
            }

            if ((httpContext.User.IsInRole("Admin") || httpContext.User.IsInRole("SystemAdministrator")))
                return true;

            return false;
        }
        /// <summary>
        /// Perform the authorization
        /// </summary>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            return IsAuthorized(httpContext);
        }
        /// <summary>
        /// Redirects request to unauthroized request page
        /// </summary>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User == null || !filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                filterContext.Result =
                    new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new { area = "", controller = "UnauthorizedRequest", action = "Index" }));
            }
        }        
        /// <summary>
        /// Permission Key attribute to be set on caller method
        /// </summary>
        public string PermissionKey { get; set; }

    }
}