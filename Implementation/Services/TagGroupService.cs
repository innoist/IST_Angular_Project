using System;
using System.Collections.Generic;
using System.Linq;
using IST.Interfaces.IServices;
using IST.Interfaces.Repository;
using IST.Models.DomainModels;
using IST.Repository.BaseRepository;

namespace IST.Implementation.Services
{
    public class TagGroupService : ITagGroupService
    {
        #region Private
        private readonly ITagGroupRepository tagGroupRepository;
        #endregion

        #region Constructor
        public TagGroupService(ITagGroupRepository tagGroupRepository)
        {
            this.tagGroupRepository = tagGroupRepository;
        }
        #endregion

        #region Public

        public IEnumerable<TagGroup> GetAll()
        {
            return tagGroupRepository.GetAllTagGroups();
        }

        public TagGroup FindTagGroupById(int tagGroupId)
        {
            return tagGroupRepository.FindTagGroupById(tagGroupId);
        }

        public bool SaveOrUpdateTagGroup(TagGroup tagGroup)
        {
            if (tagGroup.Id > 0)
            {
                var groupToUpdate = tagGroupRepository.Find(tagGroup.Id);
                groupToUpdate.DisplayValue = tagGroup.DisplayValue;
                groupToUpdate.RecLastUpdatedById = tagGroup.RecLastUpdatedById;
                groupToUpdate.RecLastUpdatedOn = tagGroup.RecLastUpdatedOn;
                tagGroupRepository.Update(groupToUpdate);
            }
            else
            {
                tagGroupRepository.Add(tagGroup);
            }
            tagGroupRepository.SaveChanges();
            return true;
        }

        public bool RemoveTagGroup(int tagGroupId)
        {
            BaseDbContext context = new BaseDbContext();
            context.DeleteTagGroup(tagGroupId);
            return true;
        }

        public bool DeleteTagGroup(int tagGroupId)
        {
            var tagGroupToDelete = tagGroupRepository.Find(tagGroupId);
            //if (tagGroupToDelete.Tags.Any())
            //{
            //    throw new Exception("You cannot delete " + tagGroupToDelete.DisplayValue + " as it is being used in Tag.");
            //}
            tagGroupRepository.Delete(tagGroupToDelete);
            tagGroupRepository.SaveChanges();
            return true;
        }

        #endregion
    }
}
