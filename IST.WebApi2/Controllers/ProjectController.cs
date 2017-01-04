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
    public class ProjectController : BaseController
    {
        #region Private
        private readonly IProjectService service;
        #endregion

        #region Constructor
        public ProjectController(IProjectService  service)
        {
            this.service = service;
        }
        #endregion

        #region Public

        [ValidateFilter]
        public IHttpActionResult Get([FromUri]ProjectSearchRequest searchRequest)
        {
            if (searchRequest == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid Bad Request");
            }
            var response = service.Search(searchRequest);
            var toReturn = new ProjectListView
            {
                Data = response.Data.ToList().Select(x => x.MapFromServerToClient()).ToList(),
                RecordsFiltered = response.FilteredCount,
                RecordsTotal = response.TotalCount
            };
            return Ok(toReturn);
        }
        
        public IHttpActionResult Delete(int id)
        {
            var result = service.Delete(id);
            return Ok(result);
        }

        #endregion
    }
}