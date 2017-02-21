using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using IST.Interfaces.IServices;
using IST.Models.Common.DropDown;
using IST.WebApi2.CustomAttributes;
using IST.WebApi2.ModelMappers;
using IST.WebApi2.Models;

namespace IST.WebApi2.Controllers
{
    public class FilterController : BaseController
    {
        #region Private
        private readonly IFilterService filterService;
        private readonly IFilterCategoryService filterCategoryService;
        #endregion

        #region Constructor
        public FilterController(IFilterService filterService, IFilterCategoryService filterCategoryService)
        {
            this.filterService = filterService;
            this.filterCategoryService = filterCategoryService;
        }
        #endregion

        #region Public

        [SiteAuthorize(PermissionKey = "FiltersDetail")]
        public IEnumerable<FilterModel> Get()
        {
            var filters = filterService.GetAll()?.Select(x => x.MapFromServerToClient()).ToList();
            return filters;
        }
        
        public IHttpActionResult Get(int id)
        {
            var toReturn = new FilterViewModel
            {
                FilterModel = filterService.FindFilterById(id)?.MapFromServerToClient(),
                FilterCategories = filterCategoryService.GetAll()?.Select(x => new DropDownModel { Id = x.Id, DisplayName = x.DisplayValue }).ToList()
            };
            return Ok(toReturn);
        }

        [HttpPost]
        [ValidateFilter]
        public IHttpActionResult PostSave(FilterModel model)
        {
            if (model.Id == 0)
                SetAllValues(model);
            else
                SetUpdatedValues(model);

            var status = filterService.SaveOrUpdateFilter(model.MapFromClientToServer());
            return Ok(status);
        }

        public IHttpActionResult DeleteSoft(int id)
        {
            var result = filterService.RemoveFilter(id);
            return Ok(result);
        }

        public IHttpActionResult DeleteCascade(int id)
        {
            var result = filterService.DeleteFilter(id);
            return Ok(result);
        }

        #endregion
    }
}