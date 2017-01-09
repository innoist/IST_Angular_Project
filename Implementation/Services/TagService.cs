using System.Collections.Generic;
using IST.Interfaces.IServices;
using IST.Interfaces.Repository;
using IST.Models.DomainModels;

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
            return tagRepository.GetAll();
        }

        public Tag FindTagById(int tagId)
        {
            return tagRepository.Find(tagId);
        }

        public bool SaveTag(Tag tag)
        {
            tagRepository.Add(tag);
            tagRepository.SaveChanges();
            return true;
        }

        public bool UpdateTag(Tag tag)
        {
            tagRepository.Update(tag);
            tagRepository.SaveChanges();
            return true;
        }

        public bool DeleteTag(int tagId)
        {
            var tagToDelete = tagRepository.Find(tagId);
            tagRepository.Delete(tagToDelete);
            tagRepository.SaveChanges();
            return true;
        }

        #endregion
    }
}
