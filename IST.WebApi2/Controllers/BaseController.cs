using System;
using System.Web.Http;
using IST.WebBase.Mvc;
using Microsoft.AspNet.Identity;

namespace IST.WebApi2.Controllers
{
    [RoutePrefix("api/Account")]
    [Authorize]
    [ApiException]
    public class BaseController : ApiController
    {
        protected void SetAllValues(Models.BaseModel model)
        {
            model.RecCreatedById = User.Identity.GetUserId();
            model.RecCreatedBy = User.Identity.Name;
            model.RecCreatedOn = DateTime.UtcNow;
            model.IsDeleted = false;
            model.IsActive = true;
            SetUpdatedValues(model);
        }

        protected void SetUpdatedValues(Models.BaseModel model)
        {
            model.RecLastUpdatedById = User.Identity.GetUserId();
            model.RecLastUpdatedBy = User.Identity.Name;
            model.RecLastUpdatedOn = DateTime.UtcNow;
        }
    }
}
