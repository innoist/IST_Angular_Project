using System.Data.Entity;
using IST.Interfaces.Repository;
using IST.Repository.BaseRepository;
using Microsoft.Practices.Unity;
using IST.Models.MenuModels;
namespace IST.Repository.Repositories
{
    /// <summary>
    /// Menu Repository
    /// </summary>
    public sealed class MenuRepository : BaseRepository<Menu>, IMenuRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MenuRepository(IUnityContainer container)
            :base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Menu> DbSet
        {
            get { return db.Menus; }
        }
        #endregion
        
    }
}
