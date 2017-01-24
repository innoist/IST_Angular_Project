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
        #endregion

        #region Constructor
        public SolutionBaseDataController(ISolutionService solutionService)
        {
            this.solutionService = solutionService;
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
    }
}
