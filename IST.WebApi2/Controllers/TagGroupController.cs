using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using IST.Interfaces.IServices;
using IST.WebApi2.Custom_Attributes;
using IST.WebApi2.ModelMappers;
using IST.WebApi2.Models;

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

        [SiteAuthorize(PermissionKey = "TagGroupsDetail")]
        public IEnumerable<TagGroupModel> Get()
        {
            var taggroups = tagGroupService.GetAll()?.Select(x => x.MapFromServerToClient()).ToList();
            return taggroups;
        }

        
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