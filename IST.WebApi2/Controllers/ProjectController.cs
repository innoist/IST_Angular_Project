using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using IST.Interfaces.IServices;
using IST.Interfaces.Repository;
using IST.Models.Common.DropDown;
using IST.Models.RequestModels;
using IST.Models.ResponseModels;
using IST.WebApi2.ModelMappers;
using IST.WebApi2.Models;
using IST.WebBase.Mvc;
using static IST.Commons.Utility.Utility;
using static System.String;
namespace IST.WebApi2.Controllers
{
    [AllowAnonymous]
    public class ProjectController : BaseController
    {
        #region Private
        private readonly ISolutionService solutionService;
        private readonly IFilterCategoryService filterCategoryService;
        private readonly IFilterService filterService;
        private readonly ISolutionTypeRepository solutionTypeRepository;
        #endregion

        #region Constructor
        public ProjectController(IFilterService filterService, ISolutionService solutionService, ISolutionTypeRepository solutionTypeRepository, IFilterCategoryService filterCategoryService)
        {
            this.filterService = filterService;
            this.solutionService = solutionService;
            this.solutionTypeRepository = solutionTypeRepository;
            this.filterCategoryService = filterCategoryService;
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
            //var filtersIds = filterService.FindFilterIdsByCategoryId(searchRequest.CategoryId);
            if (searchRequest.CategoryIds == null || !searchRequest.CategoryIds.Any())
            {
                searchRequest.CategoryIds = new List<int>();
            }
            var response = solutionService.Search(searchRequest);
            var toReturn = new ProjectListView
            {
                Data = response.Data.ToList().Select(x => x.MapFromServerToClient()).ToList(),
                SolutionTypes = solutionTypeRepository.GetAll().Select(x => x.MapFromServerToClient()).ToList(),
                FilterCategories = filterCategoryService.GetAll().Select(x => new DropDownModel { Id = x.Id, DisplayName = x.DisplayValue }).ToList(),
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
        #endregion
    }
}