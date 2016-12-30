using System;
using System.Data.Entity;
using System.Linq;
using IST.Interfaces.Repository;
using IST.Models.DomainModels;
using IST.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace IST.Repository.Repositories
{
    public sealed class WebApiUserRepository : BaseRepository<WebApiUser>, IWebApiUserRepository
    {
        #region Constructor
        /// <summary>
        /// WebApiUser Repository Constructor
        /// </summary>
        public WebApiUserRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<WebApiUser> DbSet
        {
            get
            {
                return db.WebApiUsers;
            }
        }

        #endregion

        #region Public
        /// <summary>
        /// Authenticate WebApiUser and return true if combination of username and password returns a record
        /// </summary>
        public bool AuthenticateWebApiUser(string userName, string password)
        {
            return DbSet.Count(user => user.UserName == userName && user.PasswordHash == password) > 0;
        }
        
        public WebApiUser Find(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

        
    }
}
