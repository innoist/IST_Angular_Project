using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.Http;
using IST.Interfaces.IServices;
using IST.Interfaces.Repository;
using IST.Models.RequestModels;
using IST.Models.ResponseModels;
using IST.WebApi2.ModelMappers;
using IST.WebApi2.Models;
using IST.WebBase.Mvc;
using static IST.Commons.Utility.Utility;
using static System.String;
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
            this.solutionService = service;
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
                Tags = baseData.Tags.ToList(),
                Filters = baseData.Filters.ToList(),
                SolutionTypes = baseData.SolutionTypes.ToList(),
                SolutionOwners = baseData.SolutionOwners.ToList()
            };
            return Ok(viewModel);
        }

        [HttpPost]
        [ValidateFilter]
        public IHttpActionResult Post(SolutionModel model)
        {
            if (model.Id == 0)
                SetAllValues(model);
            else
                SetUpdatedValues(model);


            //Scale Image to Aspect Ratio i.e 256*256
            if (!string.IsNullOrEmpty(model.Image))
            {
                var metaData = model.Image.Split(',')[0];
                var imageBase64 = ScaleImage(model.Image.Split(',')[1], new Size(128, 128));
                model.Image = Concat(metaData, ",", imageBase64);
            }
            SolutionCreateResponseModel response = new SolutionCreateResponseModel
            {
                Solution = model.MapFromClientToServer(),
                TagIds = model.TagIds,
                FilterIds = model.FilterIds
            };

            var status = solutionService.SaveOrUpdate(response);
            return Ok(status);
        }

        //public IHttpActionResult Delete(int id)
        //{
        //    var result = solutionService.Delete(id);
        //    return Ok(result);
        //}

        #endregion
    }
}