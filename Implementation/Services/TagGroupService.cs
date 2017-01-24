using System.Collections.Generic;
using IST.Interfaces.IServices;
using IST.Interfaces.Repository;
using IST.Models.DomainModels;

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
            return tagGroupRepository.GetAll();
        }

        public TagGroup FindTagGroupById(int tagGroupId)
        {
            return tagGroupRepository.Find(tagGroupId);
        }

        public bool SaveOrUpdateTagGroup(TagGroup tagGroup)
        {
            if (tagGroup.Id > 0)
            {
                var categoryToUpdate = tagGroupRepository.Find(tagGroup.Id);
                categoryToUpdate.DisplayValue = tagGroup.DisplayValue;
                categoryToUpdate.RecLastUpdatedById = tagGroup.RecLastUpdatedById;
                categoryToUpdate.RecLastUpdatedOn = tagGroup.RecLastUpdatedOn;
                tagGroupRepository.Update(categoryToUpdate);
            }
            else
            {
                tagGroupRepository.Add(tagGroup);
            }
            tagGroupRepository.SaveChanges();
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
