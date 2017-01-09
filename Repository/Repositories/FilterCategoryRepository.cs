using System.Data.Entity;
using IST.Interfaces.Repository;
using IST.Models.DomainModels;
using IST.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace IST.Repository.Repositories
{
    public class FilterCategoryRepository : BaseRepository<FilterCateogry> , IFilterCategoryRepository
    {
        public FilterCategoryRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<FilterCateogry> DbSet => db.FilterCategories;
    }
}
