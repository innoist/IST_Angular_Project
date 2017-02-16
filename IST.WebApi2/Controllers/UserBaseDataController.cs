using System.Linq;
using System.Web.Http;
using IST.WebApi2.Models;
using IST.Interfaces.IServices;
using IST.WebApi2.ModelMappers;
using System.Collections.Generic;

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
        public IEnumerable<RoleDDL> GetRoles()
        {
            var roles = usersService.GetAllRoles().Select(x => x.MapRoleFromServerToClient()).ToList();
            return roles;
        }

        [HttpGet]
        public IHttpActionResult GetUser(string userName)
        {
            var user = usersService.GetUser(userName)?.MapUserFromServerToClient();
            return Ok(user);
        }
        public IHttpActionResult GetUserName(string username)
        {
            var status = usersService.UsernameExistsOrNot(username);
            return Ok(status);
        }
        

    }
}
