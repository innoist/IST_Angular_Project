﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using IST.Interfaces.IServices;
using IST.WebApi2.CustomAttributes;
using IST.WebApi2.ModelMappers;
using IST.WebApi2.Models;

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
        [SiteAuthorize(PermissionKey = "FilterCategoriesDetail")]
        public IEnumerable<FilterCategoryModel> Get()
        {
            var filterCategories = filterCategoryService.GetAll()?.Select(x => x.MapFromServerToClient()).ToList();
            return filterCategories;
        }

        public FilterCategoryModel Get(int id)
        {
            var toReturn = filterCategoryService.FindFilterCategoryById(id)?.MapFromServerToClient();
            return toReturn;
        }

        [HttpPost]
        [ValidateFilter]
        public IHttpActionResult PostSave(FilterCategoryModel model)
        {
            if (model.Id == 0)
                SetAllValues(model);
            else
                SetUpdatedValues(model);

            var status = filterCategoryService.SaveOrUpdateFilterCategory(model.MapFromClientToServer());
            return Ok(status);
        }

        public IHttpActionResult DeleteSoft(int id)
        {
            var result = filterCategoryService.RemoveFilterCategory(id);
            return Ok(result);
        }

        public IHttpActionResult DeleteCascade(int id)
        {
            var result = filterCategoryService.DeleteFilterCategory(id);
            return Ok(result);
        }

        #endregion
    }
}