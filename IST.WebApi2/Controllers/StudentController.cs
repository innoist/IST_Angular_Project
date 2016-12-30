using System.Linq;
using System.Web.Http;
using IST.Interfaces.IServices;
using IST.Models.RequestModels;
using IST.WebApi2.ModelMappers;
using IST.WebApi2.Models;
using IST.WebBase.Mvc;

namespace IST.WebApi2.Controllers
{
    public class StudentController : BaseController
    {
        #region Private
        private readonly IStudentService service;
        #endregion

        #region Constructor
        public StudentController(IStudentService service)
        {
            this.service = service;
        }
        #endregion

        #region Public

        [ValidateFilter]
        public IHttpActionResult Get([FromUri]StudentSearchRequest searchRequest)
        {
            if (searchRequest == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid Bad Request");
            }
            var response = service.Search(searchRequest);
            var toReturn = new StudentLV
            {
                data = response.Data.ToList().Select(x => x.CreateFrom()).ToList(),
                recordsFiltered = response.FilteredCount,
                recordsTotal = response.TotalCount
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