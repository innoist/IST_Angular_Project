using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using IST.Interfaces.Repository;
using IST.Models.DomainModels;
using IST.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace IST.Repository.Repositories
{
    public class TagGroupRepository : BaseRepository<TagGroup>, ITagGroupRepository
    {
        public TagGroupRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<TagGroup> DbSet => db.TagGroups;

        public IEnumerable<TagGroup> GetAllTagGroups()
        {
            return DbSet.Where(x => !x.IsDeleted);
        }

        public TagGroup FindTagGroupById(int tagGroupId)
        {
            return DbSet.FirstOrDefault(x => x.Id == tagGroupId && !x.IsDeleted);
        }
    }
}
