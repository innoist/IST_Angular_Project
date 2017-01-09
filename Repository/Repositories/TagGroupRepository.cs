using System.Data.Entity;
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
    }
}
