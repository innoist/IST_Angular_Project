using System.Collections.Generic;
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
            context.RemoveTagGroup(tagGroupId);
            return true;
        }

        public bool DeleteTagGroup(int tagGroupId)
        {
            var tagGroupToDelete = tagGroupRepository.Find(tagGroupId);
            tagGroupRepository.Delete(tagGroupToDelete);
            tagGroupRepository.SaveChanges();
            return true;
        }

        #endregion
    }
}
