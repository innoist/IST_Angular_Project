﻿using System.Web.Http;
using IST.Interfaces.IServices;

namespace IST.WebApi2.Controllers
{
    public class RedirectController : BaseController
    {
        #region Private

        private readonly ISolutionService solutionService;

        #endregion

        #region Constructor

        public RedirectController(ISolutionService solutionService)
        {
            this.solutionService = solutionService;
        }

        #endregion

        #region Public

        [AllowAnonymous]
        public IHttpActionResult Get(string userId, string email, int solutionId)
        {
            string url = solutionService.GetLinkByExternalClick(userId, email, solutionId);
            System.Uri uri = new System.Uri(url);
            return Redirect(uri);
        }

        #endregion
    }
}