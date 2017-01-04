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
    public sealed class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ProjectRepository(IUnityContainer container) : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Project> DbSet => db.Projects;

        #endregion

        #region Public

        /// <summary>
        /// Search
        /// </summary>
        //public IEnumerable<Student> SearchByName(string name)
        //{
        //    var searchNameSpecified = !string.IsNullOrEmpty(name);

        //    var result = from s in DbSet
        //                 where
        //                       (string.Concat(s.FirstName, " ", s.MiddleName, string.IsNullOrEmpty(s.MiddleName) ? "" : " ",
        //                           s.LastName).ToLower().Contains(name.ToLower()) || !searchNameSpecified)
        //                 select s;

        //    return result;
        //}

        //public Student GetById(int id)
        //{
        //    return DbSet.SingleOrDefault(x => !x.IsDeleted && x.StudentId == id);
        //}

        private readonly Dictionary<OrderByProject, Func<Project, object>> orderClause =

            new Dictionary<OrderByProject, Func<Project, object>>
            {
                {OrderByProject.Name, c => c.Name}
            };

        public SearchTemplateResponse<Project> Search(ProjectSearchRequest searchRequest)
        {
            int fromRow = (searchRequest.PageNo - 1) * searchRequest.PageSize;
            int toRow = searchRequest.PageSize;
            bool searchNameSpecified = !string.IsNullOrEmpty(searchRequest.Name);
            Expression<Func<Project, bool>> query =
                s => (s.Name != null);

            //IEnumerable<Project> data = (IEnumerable<Project>)
            //    DbSet.Where(query)
            //        .Skip(fromRow)
            //        .Take(toRow).ToList();

            IEnumerable<Project> data = searchRequest.IsAsc
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

            return new SearchTemplateResponse<Project>
            {
                Data = data,
                TotalCount = DbSet.Select(x => x.Id).Count(),
                FilteredCount = data.Count()
            };
        }

        public int GetStudentCount()
        {
            return DbSet.Count(x => !x.IsDeleted);
        }
        #endregion
    }
}
