using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using IST.Interfaces.IServices;
using IST.Interfaces.Repository;
using IST.Models.RequestModels;
using IST.Models.ResponseModels;
using IST.WebApi2.ModelMappers;
using IST.WebApi2.Models;
using IST.WebBase.Mvc;
using Newtonsoft.Json;

namespace IST.WebApi2.Controllers
{
    public class SolutionController : BaseController
    {
        #region Private
        private readonly ISolutionService solutionService;
        private readonly ISolutionTypeRepository solutionTypeRepository;
        #endregion

        #region Constructor
        public SolutionController(ISolutionService service, ISolutionTypeRepository solutionTypeRepository)
        {
            solutionService = service;
            this.solutionTypeRepository = solutionTypeRepository;
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
            if (searchRequest.FilterIds == null || !searchRequest.FilterIds.Any())
            {
                searchRequest.FilterIds = new List<int>();
            }
            var response = solutionService.Search(searchRequest);
            var toReturn = new SolutionListView
            {
                Data = response.Data.ToList().Select(x => x.MapFromServerToClient()).ToList(),
                SolutionTypes = solutionTypeRepository.GetAll().Select(x => x.MapFromServerToClient()).ToList(),
                recordsFiltered = response.FilteredCount,
                recordsTotal = response.TotalCount
            };
            return Ok(toReturn);
        }

        public IHttpActionResult Get(int id)
        {
            var baseData = solutionService.GetBaseData(id);
            var viewModel = new SolutionViewModel
            {
                SolutionModel = baseData.Solution?.MapFromServerToClient(),
                Tags = baseData.Tags.Select(x => x.MapFromServerToClient()).ToList(),
                Filters = baseData.Filters.Select(x => x.MapFromServerToClient()).ToList(),
                SolutionTypes = baseData.SolutionTypes.ToList(),
                SolutionOwners = baseData.SolutionOwners.ToList()
            };
            return Ok(viewModel);
        }

        [HttpPost]
        [ValidateFilter]
        public IHttpActionResult Post()
        {
            var model = JsonConvert.DeserializeObject<SolutionModel>(HttpContext.Current.Request["1"]);
            if (HttpContext.Current.Request.Files.Count != 0)
            {
                HttpPostedFile file = HttpContext.Current.Request.Files[0];
                var fileName = DateTime.UtcNow.ToString("ddMMyyHHMMss") + file.FileName;
                file.SaveAs(HttpContext.Current.Server.MapPath(@"~\ProjectImage\" + fileName));
                model.Image = "/ProjectImage/" + fileName;
            }

            if (model.Id == 0)
                SetAllValues(model);
            else
                SetUpdatedValues(model);
            
            SolutionCreateResponseModel response = new SolutionCreateResponseModel
            {
                Solution = model.MapFromClientToServer(),
                TagIds = model.TagIds,
                FilterIds = model.FilterIds
            };

            var status = solutionService.SaveOrUpdate(response);
            return Ok(status);
        }
        public IHttpActionResult Delete(int id)
        {
            var result = solutionService.DeleteSolution(id);
            return Ok(result);
        }
        #endregion
    }
}