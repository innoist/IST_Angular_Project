using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using IST.Interfaces.IServices;
using IST.WebApi2.ModelMappers;
using IST.WebApi2.Models;
using IST.WebBase.Mvc;

namespace IST.WebApi2.Controllers
{
    public class TagGroupController : BaseController
    {
        #region Private
        private readonly ITagGroupService tagGroupService;
        #endregion

        #region Public
        public TagGroupController(ITagGroupService tagGroupService)
        {
            this.tagGroupService = tagGroupService;
        }

        // GET api/<controller>
        public IEnumerable<TagGroupModel> Get()
        {
            var taggroups = tagGroupService.GetAll()?.Select(x => x.MapFromServerToClient()).ToList();
            return taggroups;
        }

        // GET api/<controller>/5
        public TagGroupModel Get(int id)
        {
            var toReturn = tagGroupService.FindTagGroupById(id)?.MapFromServerToClient();
            return toReturn;
        }

        [HttpPost]
        [ValidateFilter]
        public IHttpActionResult Post(TagGroupModel model)
        {
            if (model.Id == 0)
                SetAllValues(model);
            else
                SetUpdatedValues(model);

            var status = tagGroupService.SaveOrUpdateTagGroup(model.MapFromClientToServer());
            return Ok(status);
        }

        //DELETE api/<controller>/5
        public IHttpActionResult DeleteSoft(int id)
        {
            var result = tagGroupService.RemoveTagGroup(id);
            return Ok(result);
        }
        public IHttpActionResult DeleteCascade(int id)
        {
            var result = tagGroupService.DeleteTagGroup(id);
            return Ok(result);
        }

        #endregion
    }
}