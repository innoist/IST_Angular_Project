using System;
using System.Collections.Generic;
using System.Linq;
using IST.Interfaces.IServices;
using IST.Interfaces.Repository;
using IST.Models.DomainModels;
using IST.Repository.BaseRepository;

namespace IST.Implementation.Services
{
    class TagService : ITagService
    {
        #region Private

        private readonly ITagRepository tagRepository;

        #endregion

        #region Constructor

        public TagService(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        #endregion

        #region Public

        public IEnumerable<Tag> GetAll()
        {
            return tagRepository.GetAllTags();
        }

        public Tag FindTagById(int tagId)
        {
            return tagRepository.FindTagById(tagId);
        }

        public bool SaveOrUpdateTag(Tag tag)
        {
            if (tag.Id > 0)
            {
                var tagToUpdate = tagRepository.Find(tag.Id);
                tagToUpdate.DisplayValue = tag.DisplayValue;
                tagToUpdate.TagGroupId = tag.TagGroupId;
                tagToUpdate.RecLastUpdatedById = tag.RecLastUpdatedById;
                tagToUpdate.RecLastUpdatedOn = tag.RecLastUpdatedOn;
                tagRepository.Update(tagToUpdate);
            }
            else
            {
                tagRepository.Add(tag);
            }
            tagRepository.SaveChanges();
            return true;
        }

        public bool RemoveTag(int tagId)
        {
            BaseDbContext context = new BaseDbContext();
            context.DeleteTag(tagId);
            return true;
        }

        public bool DeleteTag(int tagId)
        {
            var tagToDelete = tagRepository.Find(tagId);
            //if (tagToDelete.Solutions.Any())
            //{
            //    throw new Exception("You cannot delete " + tagToDelete.DisplayValue + " as it is being used in Solutions.");
            //}
            tagRepository.Delete(tagToDelete);
            tagRepository.SaveChanges();
            return true;
        }

        #endregion
    }
}
