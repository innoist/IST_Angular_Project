using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using IST.Interfaces.Repository;
using IST.Models.DomainModels;
using IST.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace IST.Repository.Repositories
{
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<Tag> DbSet => db.Tags;

        public IEnumerable<Tag> GetByTagIds(int[] ids)
        {
            return DbSet.Where(x=>ids.Contains(x.Id));
        }

        public IEnumerable<Tag> GetAllTags()
        {
            return DbSet.Where(x => !x.IsDeleted);
        }

        public Tag FindTagById(int id)
        {
            return DbSet.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        }
    }
}
