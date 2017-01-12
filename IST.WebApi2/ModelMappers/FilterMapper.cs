﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IST.Models.DomainModels;
using IST.WebApi2.Models;

namespace IST.WebApi2.ModelMappers
{
    public static class FilterMapper
    {
        public static FilterModel MapFromServerToClient(this Filter source)
        {
            return new FilterModel
            {
                Id = source.Id,
                DisplayValue = source.DisplayValue,
                FilterCategoryId = source.FilterCategoryId
            };
        }
        public static Filter MapFromClientToServer(this FilterModel source)
        {
            return new Filter
            {
                Id = source.Id,
                DisplayValue = source.DisplayValue,
                FilterCategoryId = source.FilterCategoryId
            };
        }
    }
}