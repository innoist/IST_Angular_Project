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
            Expression<Func<Solution, bool>> query;
            int fromRow = (searchRequest.PageNo - 1) * searchRequest.PageSize;
            int toRow = searchRequest.PageSize;
            if (searchRequest.ClientRequest == null)
            {
                query =
                    s => (!s.IsDeleted
                          && (searchRequest.TypeId == null || searchRequest.TypeId.Value.Equals(s.TypeId))
                          && (!searchRequest.FilterIds.Any() || s.Filters.Any(f => searchRequest.FilterIds.Contains(f.Id)))
                          && (searchRequest.OwnerId == null || searchRequest.OwnerId.Value.Equals(s.OwnerId))
                          && (searchRequest.Name == null || s.Name.ToLower().Contains(searchRequest.Name.ToLower())
                          || s.Description.ToLower().Contains(searchRequest.Name.ToLower())
                          || s.Location.ToLower().Contains(searchRequest.Name.ToLower())
                          || s.Description.ToLower().Contains(searchRequest.Name.ToLower())
                          || s.Tags.Any(x => x.DisplayValue.ToLower().Contains(searchRequest.Name.ToLower()))
                          || s.Filters.Any(x => x.DisplayValue.ToLower().Contains(searchRequest.Name.ToLower()))));
            }
            else
            {
                query =
                 s => ((s.Active.Value) && !s.IsDeleted
                       && (searchRequest.TypeId == null || searchRequest.TypeId.Value.Equals(s.TypeId))
                       && (!searchRequest.FilterIds.Any() || s.Filters.Any(f => searchRequest.FilterIds.Contains(f.Id)))
                       && (searchRequest.OwnerId == null || searchRequest.OwnerId.Value.Equals(s.OwnerId))
                       && (searchRequest.IsFavorite == false || s.AspNetUsers.FirstOrDefault() != null)
                       && (searchRequest.Name == null || s.Name.ToLower().Contains(searchRequest.Name.ToLower())
                       || s.Description.ToLower().Contains(searchRequest.Name.ToLower())
                       || s.Location.ToLower().Contains(searchRequest.Name.ToLower())
                       || s.Description.ToLower().Contains(searchRequest.Name.ToLower())
                       || s.Tags.Any(x => x.DisplayValue.ToLower().Contains(searchRequest.Name.ToLower()))
                       || s.Filters.Any(x => x.DisplayValue.ToLower().Contains(searchRequest.Name.ToLower()))));
            }
            IEnumerable<Solution> data = searchRequest.IsAsc
                ? DbSet.Include(x => x.SolutionRatings)
                    .Where(query)
                    .OrderBy(orderClause[searchRequest.OrderByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet.Include(x => x.SolutionRatings)
                    .Where(query)
                    .OrderByDescending(orderClause[searchRequest.OrderByColumn])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();

            return new SearchTemplateResponse<Solution>
            {
                Data = data,
                TotalCount = DbSet.Count(x => !x.IsDeleted),
                FilteredCount = DbSet.Count(query)
            };
        }

        public IEnumerable<Solution> SearchForTypeAhead(string name, List<int> filterIds)
        {
            var result = DbSet.Where(s => ((name == null || s.Name.ToLower().Contains(name.ToLower()))
                                       || (name == null || s.Description.ToLower().Contains(name.ToLower())))
                                       && (!s.Active.HasValue || s.Active.Value)
                                       && !s.IsDeleted
                                       && (!filterIds.Any() || s.Filters.Any(f => filterIds.Contains(f.Id))));
            return result;
        }

        #endregion
    }
}
