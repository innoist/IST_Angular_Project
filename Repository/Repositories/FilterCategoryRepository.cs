using System.Data.Entity;
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
    }
}
