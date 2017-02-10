using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using IST.Interfaces.Repository;
using IST.Models.DomainModels;
using IST.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace IST.Repository.Repositories
{
    public class FilterCategoryRepository : BaseRepository<FilterCategory> , IFilterCategoryRepository
    {
        public FilterCategoryRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<FilterCategory> DbSet => db.FilterCategories;

        public IEnumerable<FilterCategory> GetAllFilterCategories()
        {
            return DbSet.Where(x => !x.IsDeleted);
        }

        public FilterCategory FindFilterCategoryById(int filterCategoryId)
        {
            return DbSet.FirstOrDefault(x => x.Id == filterCategoryId && !x.IsDeleted);
        }
    }
}
