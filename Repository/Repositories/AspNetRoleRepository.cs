using System.Data.Entity;
using IST.Interfaces.Repository;
using IST.Models.IdentityModels;
using Microsoft.Practices.Unity;

namespace IST.Repository.Repositories
{

    public class AspNetRoleRepository : BaseRepository.BaseRepository<UserRole>, IAspNetRoleRepository
    {
        public AspNetRoleRepository(IUnityContainer container)
            : base(container)
        {
        }

        protected override IDbSet<UserRole> DbSet
        {
            get { return db.UserRoles; }
        }
    }

}
