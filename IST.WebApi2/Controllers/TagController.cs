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
    public class TagController : BaseController
    {
        #region Private
        private readonly ITagService tagService;
        private readonly ITagGroupService tagGroupService;
        #endregion

        #region Constructor
        public TagController(ITagService tagService, ITagGroupService tagGroupService)
        {
            this.tagService = tagService;
            this.tagGroupService = tagGroupService;
        }
        #endregion

        #region Public

        // GET api/<controller>
        public IEnumerable<TagModel> Get()
        {
            var tags = tagService.GetAll()?.Select(x => x.MapFromServerToClient()).ToList();
            return tags;
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var toReturn = new TagViewModel
            {
                TagModel = tagService.FindTagById(id)?.MapFromServerToClient(),
                TagGroups = tagGroupService.GetAll()?.Select(x => new DropDownModel { Id = x.Id, DisplayName = x.DisplayValue }).ToList()
            };
            return Ok(toReturn);
        }

        // POST api/<controller>
        [HttpPost]
        [ValidateFilter]
        public IHttpActionResult Post(TagModel model)
        {
            if (model.Id == 0)
                SetAllValues(model);
            else
                SetUpdatedValues(model);

            var status = tagService.SaveOrUpdateTag(model.MapFromClientToServer());
            return Ok(status);
        }

        public IHttpActionResult DeleteSoft(int id)
        {
            var result = tagService.RemoveTag(id);
            return Ok(result);
        }

        public IHttpActionResult DeleteCascade(int id)
        {
            var result = tagService.DeleteTag(id);
            return Ok(result);
        }

        #endregion
    }
}