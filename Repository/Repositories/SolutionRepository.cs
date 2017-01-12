using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using IST.Interfaces.Repository;
using IST.Models.Common;
using IST.Models.DomainModels;
using IST.Models.RequestModels;
using IST.Models.ResponseModels;
using IST.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace IST.Repository.Repositories
{
    /// <summary>
    /// Project Repository
    /// </summary>
    public sealed class SolutionRepository : BaseRepository<Solution>, ISolutionRepository
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SolutionRepository(IUnityContainer container) : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Solution> DbSet => db.Projects;

        #endregion

        #region Public

        public Solution GetById(int id)
        {
            return DbSet.SingleOrDefault(x => x.Id == id);
        }

        private readonly Dictionary<OrderBySolution, Func<Solution, object>> orderClause =

            new Dictionary<OrderBySolution, Func<Solution, object>>
            {
                {OrderBySolution.Name, c => c.Name}
            };

        public SearchTemplateResponse<Solution> Search(SolutionSearchRequest searchRequest)
        {
            int fromRow = (searchRequest.PageNo - 1) * searchRequest.PageSize;
            int toRow = searchRequest.PageSize;
            bool searchNameSpecified = !string.IsNullOrEmpty(searchRequest.Name);
            Expression<Func<Solution, bool>> query =
                s => ((searchRequest.TypeId == null || searchRequest.TypeId.Value.Equals(s.TypeId)) &&
                      (!searchRequest.CategoryIds.Any() || s.Filters.Any(x => searchRequest.CategoryIds.Any(y => y.Equals(x.FilterCategoryId)))) &&
                      (searchRequest.OwnerId == null || searchRequest.OwnerId.Value.Equals(s.OwnerId)) &&
                      (searchRequest.Name == null || s.Name.ToLower().Contains(searchRequest.Name.ToLower())
                      || s.Tags.Any(x=>x.DisplayValue.Contains(searchRequest.Name.ToLower()))
                      || s.Tags.Any(x => x.TagGroup.DisplayValue.Contains(searchRequest.Name.ToLower()))));

            var aa = DbSet.Where(s => s.Filters.Any(x => searchRequest.CategoryIds.Any(y => y.Equals(x.FilterCategoryId))) && searchRequest.CategoryIds.Any()).ToList();
            var aaa = DbSet.Where(s => s.Tags.Any(x => x.DisplayValue.Contains(searchRequest.Name.ToLower()))
                      || s.Tags.Any(x => x.TagGroup.DisplayValue.Contains(searchRequest.Name.ToLower())));
            var b = DbSet.Where(s => s.Name.ToLower().Contains(searchRequest.Name.ToLower()));
            IEnumerable<Solution> data = searchRequest.IsAsc
                ? DbSet
                    .Where(query)
                    .OrderBy(orderClause[searchRequest.OrderByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet
                    .Where(query)
                    .OrderByDescending(orderClause[searchRequest.OrderByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();

            return new SearchTemplateResponse<Solution>
            {
                Data = data,
                TotalCount = DbSet.Select(x => x.Id).Count(),
                FilteredCount = DbSet.Count(query)
            };
        }

        #endregion
    }
}
