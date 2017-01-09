using System.Linq;
using System.Web.Http;
using IST.Interfaces.IServices;
using IST.Models.RequestModels;
using IST.WebApi2.ModelMappers;
using IST.WebApi2.Models;
using IST.WebBase.Mvc;

namespace IST.WebApi2.Controllers
{
    [AllowAnonymous]
    public class SolutionController : BaseController
    {
        #region Private
        private readonly ISolutionService projectService;
        #endregion

        #region Constructor
        public SolutionController(ISolutionService  service)
        {
            this.projectService = service;
        }
        #endregion

        #region Public

        [ValidateFilter]
        public IHttpActionResult Get([FromUri]SolutionSearchRequest searchRequest)
        {
            if (searchRequest == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid Bad Request");
            }
            var response = projectService.Search(searchRequest);
            var toReturn = new ProjectListView
            {
                Data = response.Data.ToList().Select(x => x.MapFromServerToClient()).ToList(),
                RecordsFiltered = response.FilteredCount,
                RecordsTotal = response.TotalCount
            };
            return Ok(toReturn);
        }

        public IHttpActionResult Get(int id)
        {
            var toReturn = projectService.GetById(id).MapFromServerToClient();
            return Ok(toReturn);
        }

        public IHttpActionResult Delete(int id)
        {
            var result = projectService.Delete(id);
            return Ok(result);
        }

        #endregion
    }
}