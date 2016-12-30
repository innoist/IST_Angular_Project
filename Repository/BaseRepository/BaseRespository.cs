using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using IST.Commons;
using IST.Interfaces.Repository;
using IST.Models.IdentityModels;
using Microsoft.Practices.Unity;

namespace IST.Repository.BaseRepository
{
    /// <summary>
    /// Base Repository
    /// </summary>
    public abstract class BaseRepository<TDomainClass> : IBaseRepository<TDomainClass, long>
       where TDomainClass : class
    {
        #region Private

        // ReSharper disable once InconsistentNaming
        private readonly IUnityContainer container;
        #endregion
        #region Protected
        /// <summary>
        /// Primary database set
        /// </summary>
        protected abstract IDbSet<TDomainClass> DbSet { get; }

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        protected BaseRepository(IUnityContainer container)
        {

            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.container = container;
            string connectionString = ConfigurationManager.ConnectionStrings["BaseDbContext"].ConnectionString;
            db = (BaseDbContext)container.Resolve(typeof(BaseDbContext), new ResolverOverride[] { new ParameterOverride("connectionString", connectionString) });
        }

        #endregion
        #region Public
        /// <summary>
        /// base Db Context
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public BaseDbContext db;
       
        /// <summary>
        /// Create object instance
        /// </summary>
        public virtual TDomainClass Create()
        {
            TDomainClass result = container.Resolve<TDomainClass>();
            return result;
        }
        /// <summary>
        /// Find entry by key
        /// </summary>
        public virtual IQueryable<TDomainClass> Find(TDomainClass instance)
        {
            return DbSet.Find(instance) as IQueryable<TDomainClass>;
        }
        /// <summary>
        /// Find Entity by Id
        /// </summary>
        public virtual TDomainClass Find(long id)
        {
            return DbSet.Find(id);
        }
        /// <summary>
        /// Get All Entites 
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TDomainClass> GetAll()
        {
            return DbSet;
        }
        /// <summary>
        /// Save Changes in the entities
        /// </summary>
        public void SaveChanges()
        {
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = new List<string>();
                foreach (DbEntityValidationResult validationResult in ex.EntityValidationErrors)
                {
                    var entityName = validationResult.Entry.Entity.GetType().Name;
                    errorMessages.AddRange(validationResult.ValidationErrors.Select(error => entityName + "." + error.PropertyName + ": " + error.ErrorMessage));
                }
                // Throw Exception
                throw new Exception(string.Format(CultureInfo.InvariantCulture, Resources.General.FailedToSave));
            }
        }
        /// <summary>
        /// Delete an entry
        /// </summary>
        public virtual void Delete(TDomainClass instance)
        {
            DbSet.Remove(instance);
        }
        /// <summary>
        /// Add an entry
        /// </summary>
        public virtual void Add(TDomainClass instance)
        {
            DbSet.Add(instance);
        }
        /// <summary>
        /// Add an entry
        /// </summary>
        public virtual void Update(TDomainClass instance)
        {
            DbSet.AddOrUpdate(instance);
        }

        /// <summary>
        /// Eager load property
        /// </summary>
        public void LoadProperty<T>(object entity, Expression<Func<T>> propertyExpression, bool isCollection = false)
        {
            db.LoadProperty(entity, propertyExpression, isCollection);
        }
        
        /// <summary>
        /// Logged in User Identity
        /// </summary>
        public string LoggedInUserIdentity
        {
            get
            {
                Claim domainKeyClaim = ClaimHelper.GetClaimToString(ClaimTypes.Name);
                if (domainKeyClaim == null)
                {
                    throw new InvalidOperationException("User Domain Name claim not found!");
                }
                return (domainKeyClaim.Value);
            }
        }

        /// <summary>
        /// User Timezone OffSet
        /// </summary>
        public TimeSpan UserTimezoneOffSet
        {
            get
            {
                Claim userTimeZoneOffsetClaim = ClaimHelper.GetClaimToString(ISTUserClaims.UserTimeZoneOffset);
                if (userTimeZoneOffsetClaim == null)
                {
                    return TimeSpan.FromMinutes(0);
                }

                TimeSpan userTimeZoneOffset;
                TimeSpan.TryParse(userTimeZoneOffsetClaim.Value, out userTimeZoneOffset);

                return userTimeZoneOffset;
            }
        }

        /// <summary>
        /// All Role in DB
        /// </summary>
        public IEnumerable<UserRole> Roles()
        {
            return db.UserRoles.Where(r => !r.Name.Equals(ISTApplicationRoles.SystemAdministrator));
        }

        #endregion
    }
}