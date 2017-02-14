using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IST.Interfaces.Repository;
using IST.Models.Common;
using IST.Models.IdentityModels;
using IST.Models.RequestModels;
using IST.Models.ResponseModels;
using IST.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace IST.Repository.Repositories
{
    /// <summary>
    /// User repository for User related functions
    /// </summary>
    public class UserRepository : BaseRepository<AspNetUser>, IUserRepository
    {
        #region private

        private readonly Dictionary<OrderByUsers, Func<AspNetUser, object>> orderClause =

            new Dictionary<OrderByUsers, Func<AspNetUser, object>>
            {
                //{OrderByUsers.Name, c => c.},
                {OrderByUsers.UserName, c => c.UserName},
                {OrderByUsers.FirstName, c => c.FirstName},
                {OrderByUsers.Address, c => c.Address},
                {OrderByUsers.Email, c => c.Email}
            };

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public UserRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<AspNetUser> DbSet
        {
            get
            {
                return db.Users;
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// To get the maximum user domain key
        /// </summary>
        public double GetMaxUserDomainKey()
        {
            return DbSet.Max(user => user.UserDomainKey);
        }
        /// <summary>
        /// Returns User by user Id
        /// </summary>
        public AspNetUser FindUserById(string userId)
        {
            return DbSet.SingleOrDefault(user => user.Id == userId);
        }
        public AspNetUser FindUserByUserName(string userName)
        {
            return DbSet.FirstOrDefault(user => user.UserName == userName);
        }

        public bool UsernameExistsOrNot(string username)
        {
            return DbSet.Any(x => x.UserName.ToLower().Contains(username.ToLower()));
        }
        /// <summary>
        /// Get User By Domain Key
        /// </summary>
        public AspNetUser GetLoggedInUser()
        {
            return DbSet.FirstOrDefault(user => user.UserName == LoggedInUserIdentity);
        }

        public UsersSearchResponse GetUsersSearchResponse(UsersSearchRequest searchRequest)
        {
            int fromRow = (searchRequest.PageNo - 1) * searchRequest.PageSize;
            int toRow = searchRequest.PageSize;
            Expression<Func<AspNetUser, bool>> query =
                s =>
                    (
                    searchRequest.Name == null || s.FirstName.Contains(searchRequest.Name) || s.LastName.Contains(searchRequest.Name)) &&
                    (searchRequest.Username == null || s.UserName.Contains(searchRequest.Username)) &&
                    (searchRequest.Email == null || s.Email.Contains(searchRequest.Email));
            //(searchRequest.UserRole == "SystemAdministrator" && s.AspNetRoles.All(x => x.Name == "Admin") || s.AspNetRoles.All(x => x.Name == "Client")
            IEnumerable<AspNetUser> users = searchRequest.IsAsc
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
            return new UsersSearchResponse { Users = users, TotalCount = DbSet.Count(query), FilteredCount = DbSet.Count(query) };
        }

        public async Task<AspNetUser> GetByUserNameAsync(string userName)
        {
            return await DbSet.SingleOrDefaultAsync(x => x.UserName == userName);
        }
        #endregion
    }
}
