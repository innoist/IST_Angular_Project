using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using IST.Interfaces.IServices;
using IST.WebApi2.ModelMappers;
using IST.WebApi2.Models;
using IST.WebBase.Mvc;

namespace IST.WebApi2.Controllers
{
    public class FilterCategoryController : BaseController
    {
        #region Private
        private readonly IFilterCategoryService filterCategoryService;
        #endregion

        #region Public
        public FilterCategoryController(IFilterCategoryService filterCategoryService)
        {
            this.filterCategoryService = filterCategoryService;
        }

        // GET api/<controller>
        public IEnumerable<FilterCategoryModel> Get()
        {
            var filterCategories = filterCategoryService.GetAll()?.Select(x => x.MapFromServerToClient()).ToList();
            return filterCategories;
        }

        // GET api/<controller>/5
        public FilterCategoryModel Get(int id)
        {
            var toReturn = filterCategoryService.FindFilterCategoryById(id)?.MapFromServerToClient();
            return toReturn;
        }

        // POST api/<controller>
        [System.Web.Http.HttpPost]
        [ValidateFilter]
        public IHttpActionResult Post(FilterCategoryModel model)
        {
            if (model.Id == 0)
                SetAllValues(model);
            else
                SetUpdatedValues(model);
            
            var status = filterCategoryService.SaveOrUpdateFilterCategory(model.MapFromClientToServer());
            return Ok(status);
        }

        //DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            var result = filterCategoryService.DeleteFilterCategory(id);
            return Ok(result);
        }

        #endregion
    }
}