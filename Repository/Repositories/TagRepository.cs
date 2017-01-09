using System.Data.Entity;
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
    }
}
