using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using IST.Interfaces.IServices;
using IST.WebApi2.ModelMappers;
using IST.WebApi2.Models;
using IST.WebBase.Mvc;

namespace IST.WebApi2.Controllers
{
    public class SolutionOwnerController : BaseController
    {
        #region Private

        private readonly ISolutionOwnerService solutionOwnerService;

        #endregion

        #region Constructor

        public SolutionOwnerController(ISolutionOwnerService solutionOwnerService)
        {
            this.solutionOwnerService = solutionOwnerService;
        }

        #endregion

        #region Public

        #region Index

        public IEnumerable<SolutionOwnerModel> Get()
        {
            var models = solutionOwnerService.GetAll()?.Select(x => x.MapFromServerToClient()).ToList();
            return models;
        }

        #endregion

        #region Create/Update

        public SolutionOwnerModel Get(int id)
        {
            var toReturn = solutionOwnerService.FindSolutionOwnerById(id)?.MapFromServerToClient();
            return toReturn;
        }

        [HttpPost]
        [ValidateFilter]
        public IHttpActionResult Post(SolutionOwnerModel model)
        {
            if (model.Id == 0)
                SetAllValues(model);
            else
                SetUpdatedValues(model);

            var status = solutionOwnerService.SaveOrUpdate(model.MapFromClientToServer());
            return Ok(status);
        }

        #endregion

        #region Delete

        public IHttpActionResult DeleteSoft(int id)
        {
            var result = solutionOwnerService.RemoveSolutionOwner(id);
            return Ok(result);
        }
        public IHttpActionResult DeleteCascade(int id)
        {
            var result = solutionOwnerService.DeleteSolutionOwner(id);
            return Ok(result);
        }

        #endregion

        #endregion
    }
}