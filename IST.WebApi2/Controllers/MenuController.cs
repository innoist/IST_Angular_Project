using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using IST.Interfaces.IServices;
using IST.WebBase.Mvc;
using Microsoft.AspNet.Identity.Owin;

namespace IST.WebApi2.Controllers
{
    public class MenuController : BaseController
    {
        #region Private

        private readonly IMenuRightsService menuRightsService;

        private Implementation.Identity.ApplicationUserManager UserManager
            => Request.GetOwinContext().GetUserManager<Implementation.Identity.ApplicationUserManager>();

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public MenuController(IMenuRightsService menuRightsService)
        {
            if (menuRightsService == null)
            {
                throw new ArgumentNullException("menuRightsService");
            }

            this.menuRightsService = menuRightsService;
        }

        #endregion

        #region Public
        /// <summary>
        /// Recieves the timezone offset value as id
        /// </summary>
        /// <returns>Menu entries list</returns>
        [Authorize]
        [ApiException]
        public IHttpActionResult Get()
        {
            //Fetch menu list
            var menu = menuRightsService.GetForRole().ToList();

            //if user is admin, then replace users menu text with chefs
            //menu.ForEach(x => x.text = User.IsInRole(ISTApplicationRoles.Admin) && x.text.ToLower().Contains("user") ? x.text = "Chefs" : x.text);

            return Ok(menu);
        }

        #endregion
    }
}
