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
        private readonly IUsageHistoryService usageService;
        #endregion

        #region Constructor
        public ProjectController(ISolutionService solutionService, IFilterCategoryService filterCategoryService, ISolutionRatingService solutionRatingService, IUsageHistoryService usageService)
        {
            this.solutionService = solutionService;
            this.filterCategoryService = filterCategoryService;
            this.solutionRatingService = solutionRatingService;
            this.usageService = usageService;
        }
        #endregion

        #region Public

        [ValidateFilter]
        public IHttpActionResult GetSolutions([FromUri]SolutionSearchRequest searchRequest)
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
                recordsFiltered = response.FilteredCount,
                recordsTotal = response.TotalCount
            };
            return Ok(toReturn);
        }

        public IHttpActionResult GetFilterCategories()
        {
            var toreturn =
                filterCategoryService.GetAll()
                    .Select(x => x.MapFromServerToClient())
                    .Where(x => x.Filters.Count > 0)
                    .ToList();
            return Ok(toreturn);
        }
        public IHttpActionResult GetById(int id)
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
        public IHttpActionResult GetForTypeAhead(string name, [FromUri]List<int> filterIds)
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
        public IHttpActionResult PostFavoriteSolution(SolutionModel model)
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

        [HttpPost]
        public IHttpActionResult PostSolutionRating(SolutionRatingModel model)
        {
            if (model.RatingId == 0)
                SetAllValues(model);
            else
                SetUpdatedValues(model);
            var result = solutionRatingService.SaveOrUpdate(model.MapFromClientToServer());
            return Ok(result);
        }
        
        public IHttpActionResult PostClickActivity(SolutionModel model)
        {
            var saved = usageService.SaveUsage(model.Id, (int)Commons.UsageType.Clicked, null, null, null);
            return Ok(saved);
        }
        
        [HttpPost]
        public IHttpActionResult PostShareActivity(EmailModel model)
        {
            var saved = usageService.SaveUsage(model.SolutionId, (int)Commons.UsageType.Shared, model.RecieverEmail, model.EmailSubject, model.EmailBody);
            return Ok(saved);
        }
        #endregion
    }
}