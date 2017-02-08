using System.Linq;
using System.Web.Http;
using IST.Interfaces.IServices;
using IST.Models.Common.DropDown;
using IST.Models.RequestModels;
using IST.WebApi2.Models;

namespace IST.WebApi2.Controllers
{
    public class SolutionBaseDataController : BaseController
    {
        #region Private

        private readonly ISolutionService solutionService;
        private readonly IUsageHistoryService usageService;

        #endregion

        #region Constructor
        public SolutionBaseDataController(ISolutionService solutionService, IUsageHistoryService usageService)
        {
            this.solutionService = solutionService;
            this.usageService = usageService;
        }

        #endregion
        // GET: SolutionBaseData

        public IHttpActionResult Get()
        {
            var baseData = solutionService.GetFilterData();
            var toReturn = new SolutionViewModel
            {
                SolutionTypes = baseData.SolutionTypes.ToList(),
            };
            return Ok(toReturn);
        }

        /// <summary>
        /// For Click Activity (Client Side)
        /// </summary>
        public IHttpActionResult Post(SolutionModel model)
        {
            usageService.SaveUsage(model.Id, (int)Commons.UsageType.Clicked, null, null);
            return Ok();
        }

        /// <summary>
        /// For Share Activity (Client Side)
        /// </summary>
        [Route("api/ShareActivity")]
        [HttpPost]
        public IHttpActionResult Post(EmailModel model)
        {
            var saved = usageService.SaveUsage(model.SolutionId, (int)Commons.UsageType.Shared, model.RecieverEmail, model.EmailBody);
            //var status = Commons.Utility.Utility.SendEmailAsync(model.RecieverEmail, model.EmailBody);
            return Ok(saved);
        }
    }
}
