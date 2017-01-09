using System.Data.Entity;
using IST.Interfaces.Repository;
using IST.Models.DomainModels;
using IST.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace IST.Repository.Repositories
{
    public class FilterCategory : BaseRepository<Filter>, IFilterRepository
    {
        public FilterCategory(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<Filter> DbSet => db.Filters;
    }
}
