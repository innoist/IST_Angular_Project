using System;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace IST.WebApi2.Controllers
{
    [RoutePrefix("api/Account")]
    [Authorize]
    [CustomAttributes.ApiException]
    public class BaseController : ApiController
    {
        protected void SetAllValues(Models.BaseModel model)
        {
            model.RecCreatedById = User.Identity.GetUserId();
            model.RecCreatedOn = DateTime.UtcNow;
            SetUpdatedValues(model);
        }

        protected void SetUpdatedValues(Models.BaseModel model)
        {
            model.RecLastUpdatedById = User.Identity.GetUserId();
            model.RecLastUpdatedOn = DateTime.UtcNow;
        }
    }
}
