using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using IST.Interfaces.IServices;
using IST.Models.Common.DropDown;
using IST.Models.RequestModels;
using IST.WebApi2.CustomAttributes;
using IST.WebApi2.ModelMappers;
using IST.WebApi2.Models;

namespace IST.WebApi2.Controllers
{

    //**********************************************//
                    //For Client Side//
    //**********************************************//
    public class ProjectController : BaseController
    {
        #region Private
        private readonly ISolutionService solutionService;
        private readonly IFilterCategoryService filterCategoryService;
        private readonly ISolutionRatingService solutionRatingService;

        #endregion

        #region Constructor
        public ProjectController(ISolutionService solutionService, IFilterCategoryService filterCategoryService, ISolutionRatingService solutionRatingService)
        {
            this.solutionService = solutionService;
            this.filterCategoryService = filterCategoryService;
            this.solutionRatingService = solutionRatingService;
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
            var toReturn = new ProjectListView
            {
                Data = response.Data.ToList().Select(x => x.ClientCreateFrom()).ToList(),
                FilterCategories = filterCategoryService.GetAll().Select(x => x.MapFromServerToClient()).Where(x => x.Filters.Count > 0).ToList(),
                recordsFiltered = response.FilteredCount,
                recordsTotal = response.TotalCount
            };
            return Ok(toReturn);
        }
        public IHttpActionResult Get(int id)
        {
            var baseData = solutionService.GetProjectBaseData(id);
            var viewModel = new ProjectViewModel
            {
                SolutionModel = baseData.Solution?.ClientCreateFrom(),
                SolutionRatings = baseData.SolutionRatings.Select(x => x.MapFromServerToClient()).ToList()
            };
            return Ok(viewModel);
        }

        #region TypeAhead
        [Route("api/ProjectBaseData")]
        public IHttpActionResult Get(string name, [FromUri]List<int> filterIds)
        {
            var response = solutionService.SearchForTypeAhead(name, filterIds).ToList().Select(s => new DropDownModel
            {
                DisplayName = s.Name,
                Id = s.Id,
                Subtitle = s.Description
            });
            return Ok(response);
        }
        #endregion

        [HttpPost]
        public IHttpActionResult Post(SolutionModel model)
        {
            if (model.IsFavorite)
            {
                //if solution is already favorite, remove it from favorite
                var status = solutionService.SaveFavorite(model.Id, false);
                return Ok(status);
            }
            else
            {
                //if solution is not already favorite, mark it favorite
                var status = solutionService.SaveFavorite(model.Id, true);
                return Ok(status);
            }
        }

        [Route("api/ProjectBaseData")]
        [HttpPost]
        public IHttpActionResult Post(SolutionRatingModel model)
        {
            if (model.RatingId == 0)
                SetAllValues(model);
            else
                SetUpdatedValues(model);
            var result = solutionRatingService.SaveOrUpdate(model.MapFromClientToServer());
            return Ok(result);
        }


        #endregion
    }
}