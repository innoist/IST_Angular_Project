using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using IST.Interfaces.Repository;
using IST.Models.DomainModels;
using IST.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace IST.Repository.Repositories
{
    public class FilterRepository : BaseRepository<Filter>, IFilterRepository
    {
        public FilterRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<Filter> DbSet => db.Filters;

        public IEnumerable<Filter> GetByFilterIds(int[] ids)
        {
            return DbSet.Where(x => ids.Contains(x.Id));
        }

        public IEnumerable<Filter> GetAllFilters()
        {
            return DbSet.Where(x => !x.IsDeleted);
        }

        public Filter FindFilterById(int filterId)
        {
            return DbSet.FirstOrDefault(x => x.Id == filterId && !x.IsDeleted);
        }
    }
}
