using System.Data.Entity;
using System.Linq;
using IST.Interfaces.Repository;
using IST.Models.DomainModels;
using IST.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace IST.Repository.Repositories
{
    /// <summary>
    /// User details repository related to Registerd user details
    /// </summary>
    public class UserDetailsRepository : BaseRepository<UserDetail>, IUserDetailsRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public UserDetailsRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<UserDetail> DbSet
        {
            get
            {
                return db.UserDetails;
            }
        }

        #endregion
        #region Public
        /// <summary>
        /// Finds the User details by user id
        /// </summary>
        public UserDetail FindByUserId(string userId)
        {
            return DbSet.FirstOrDefault(userDetails => userDetails.UserId == userId);
        }

        /// <summary>
        /// Executes 
        /// </summary>
        public void CopyUserDefaultData(string userId, long domainKey)
        {
            db.ExecuteCreateDefaultData(userId, domainKey);
        }

        /// <summary>
        /// To check if URL is available 
        /// </summary>
        public bool IsCompanyUrlAvailable(string url)
        {
           return (! DbSet.Any(userDetails => userDetails.CompanyShortUrl == url));
        }

        /// <summary>
        /// Get User Id for URL 
        /// </summary>
        public string GetUserIdByCompanyUrl(string url)
        {
            var firstOrDefault = DbSet.FirstOrDefault(detail => detail.CompanyShortUrl.ToUpper().Equals(url));
            if (firstOrDefault != null)
                return firstOrDefault.UserId;
            return null;
        }

        #endregion
    }
}
