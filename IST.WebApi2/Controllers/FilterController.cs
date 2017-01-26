using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using IST.Interfaces.IServices;
using IST.Models.Common.DropDown;
using IST.WebApi2.ModelMappers;
using IST.WebApi2.Models;
using IST.WebBase.Mvc;

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

        // GET api/<controller>
        public IEnumerable<FilterModel> Get()
        {
            var filters = filterService.GetAll()?.Select(x => x.MapFromServerToClient()).ToList();
            return filters;
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var toReturn = new FilterViewModel
            {
                FilterModel = filterService.FindFilterById(id)?.MapFromServerToClient(),
                FilterCategories = filterCategoryService.GetAll()?.Select(x => new DropDownModel { Id = x.Id, DisplayName = x.DisplayValue }).ToList()
            };
            return Ok(toReturn);
        }

        // POST api/<controller>
        [HttpPost]
        [ValidateFilter]
        public IHttpActionResult Post(FilterModel model)
        {
            if (model.Id == 0)
                SetAllValues(model);
            else
                SetUpdatedValues(model);

            var status = filterService.SaveOrUpdateFilter(model.MapFromClientToServer());
            return Ok(status);
        }

        //DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            var result = filterService.DeleteFilter(id);
            return Ok(result);
        }

        #endregion
    }
}