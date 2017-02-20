using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using IST.Interfaces.IServices;
using IST.WebApi2.Custom_Attributes;
using IST.WebApi2.ModelMappers;
using IST.WebApi2.Models;

namespace IST.WebApi2.Controllers
{
    public class SolutionTypeController : BaseController
    {
        #region Private

        private readonly ISolutionTypeService solutionTypeService;

        #endregion

        #region Constructor

        public SolutionTypeController(ISolutionTypeService solutionTypeService)
        {
            this.solutionTypeService = solutionTypeService;
        }

        #endregion

        #region Public

        #region Index

        public IEnumerable<SolutionTypeModel> Get()
        {
            var models = solutionTypeService.GetAll()?.Select(x => x.MapFromServerToClient()).ToList();
            return models;
        }

        #endregion

        #region Create/Update

        public SolutionTypeModel Get(int id)
        {
            var toReturn = solutionTypeService.FindSolutionTypeById(id)?.MapFromServerToClient();
            return toReturn;
        }

        [HttpPost]
        [ValidateFilter]
        public IHttpActionResult Post(SolutionTypeModel model)
        {
            if (model.Id == 0)
                SetAllValues(model);
            else
                SetUpdatedValues(model);

            var status = solutionTypeService.SaveOrUpdate(model.MapFromClientToServer());
            return Ok(status);
        }

        #endregion

        #region Delete

        public IHttpActionResult DeleteSoft(int id)
        {
            var result = solutionTypeService.RemoveSolutionType(id);
            return Ok(result);
        }
        public IHttpActionResult DeleteCascade(int id)
        {
            var result = solutionTypeService.DeleteSolutionType(id);
            return Ok(result);
        }

        #endregion

        #endregion
    }
}