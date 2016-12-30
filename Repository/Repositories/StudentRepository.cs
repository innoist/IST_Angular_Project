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
    /// Supplier Repository
    /// </summary>
    public sealed class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public StudentRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Student> DbSet => db.Students;

        #endregion

        #region Public

        /// <summary>
        /// Search
        /// </summary>
        public IEnumerable<Student> SearchByName(string name)
        {
            var searchNameSpecified = !string.IsNullOrEmpty(name);

            var result = from s in DbSet
                         where
                               (string.Concat(s.FirstName, " ", s.MiddleName, string.IsNullOrEmpty(s.MiddleName) ? "" : " ",
                                   s.LastName).ToLower().Contains(name.ToLower()) || !searchNameSpecified)
                         select s;

            return result;
        }

        public Student GetById(int id)
        {
            return DbSet.SingleOrDefault(x => !x.IsDeleted && x.StudentId == id);
        }

        private readonly Dictionary<OrderByStudent, Func<Student, object>> orderClause =

            new Dictionary<OrderByStudent, Func<Student, object>>
            {
                {OrderByStudent.Name, c => c.FirstName},
                {OrderByStudent.DOB, c => c.DateOfBirth},
                {OrderByStudent.GuardianName, c => c.GuardianFirstName},
                {OrderByStudent.GuardianPhone, c => c.GuardianPhone},
                {OrderByStudent.GuardianEmail, c => c.GuardianEmail}
            };

        public SearchTemplateResponse<Student> Search(StudentSearchRequest searchRequest)
        {
            int fromRow = (searchRequest.PageNo - 1) * searchRequest.PageSize;
            int toRow = searchRequest.PageSize;
            bool searchNameSpecified = !string.IsNullOrEmpty(searchRequest.Name);
            Expression<Func<Student, bool>> query =
                s =>
                    (
                     (searchRequest.StudentId == 0 || searchRequest.StudentId.Equals(s.StudentId)) &&
                     (searchRequest.DateOfBirth == null ||
                      DbFunctions.TruncateTime(searchRequest.DateOfBirth.Value) ==
                      DbFunctions.TruncateTime(s.DateOfBirth)) &&
                     (searchNameSpecified &&
                      String.Concat(s.FirstName, " ", s.MiddleName, string.IsNullOrEmpty(s.MiddleName) ? "" : " ",
                          s.LastName).ToLower().Contains(searchRequest.Name.ToLower()) || !searchNameSpecified) &&
                     (!s.IsDeleted)
                        );

            IEnumerable<Student> data = searchRequest.IsAsc
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
            return new SearchTemplateResponse<Student>
            {
                Data = data,
                TotalCount = DbSet.Count(x => !x.IsDeleted),
                FilteredCount = DbSet.Count(query)
            };
        }

        public IEnumerable<Student> GetStudentsByIds(int[] ids)
        {
            return DbSet.Where(x => !x.IsDeleted && ids.Contains(x.StudentId));
        }

        public int GetStudentCount()
        {
            return DbSet.Count(x => !x.IsDeleted);
        }
        #endregion
    }
}
