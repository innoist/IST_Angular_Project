using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using IST.Interfaces.IServices;
using IST.WebApi2.ModelMappers;
using IST.WebApi2.Models;
using IST.WebBase.Mvc;

namespace IST.WebApi2.Controllers
{
    public class AllergyController : BaseController
    {
        #region Private
        private readonly IAllergyService allergyService;
        #endregion

        #region Public
        public AllergyController(IAllergyService allergyService)
        {
            this.allergyService = allergyService;
        }

        //GET api/<controller>
        public IEnumerable<AllergyModel> Get()
        {
            var allergies = allergyService.GetAllergies().ToList().Select(x => x.MapFromServerToClient()).ToList();
            return allergies;
        }

        // GET api/<controller>/5
        public AllergyModel Get(int id)
        {
            var allergy = allergyService.GetAllergyById(id)?.MapFromServerToClient();
            return allergy;
        }

        

        // POST api/<controller>
        [HttpPost]
        [ValidateFilter]
        public IHttpActionResult Post(AllergyModel model)
        {
            if (model.AllergyId == 0)
                SetAllValues(model);
            else
                SetUpdatedValues(model);
            

            var status = allergyService.SaveOrUpdate(model.MapFromClientToServer());
            return Ok(status);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            var result = allergyService.Delete(id);
            return Ok(result);
        }

        #endregion
    }
}