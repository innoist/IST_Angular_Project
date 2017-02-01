using System.Web.Http;
using IST.Interfaces.IServices;
using IST.WebApi2.ModelMappers;
using IST.WebApi2.Models;

namespace IST.WebApi2.Controllers
{
    public class UserBaseDataController : BaseController
    {

        #region Private

        private readonly IUsersService usersService;

        public UserBaseDataController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        #endregion

        // GET: api/UserBaseData
        //[HttpGet]
        //public IEnumerable<RoleDDL> Get()
        //{
        //    var roles = usersService.GetAllRoles().Select(x => x.MapRoleFromServerToClient()).ToList();
        //    return roles;
        //}

        [HttpGet]
        public IHttpActionResult Get(string userName)
        {
            var user = usersService.GetUser(userName)?.MapUserFromServerToClient();
            return Ok(user);
        }
        
    }
}
