using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using IST.Interfaces.IServices;
using IST.Models.RequestModels;
using IST.WebApi2.Custom_Attributes;
using IST.WebApi2.ModelMappers;
using IST.WebApi2.Models;

namespace IST.WebApi2.Controllers
{
    public class UsersController : BaseController
    {
        #region Private

        private readonly IUsersService usersService;

        #endregion

        #region Constructor
        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        #endregion

        #region Public

        [SiteAuthorize(PermissionKey = "UsersDetail")]
        public UsersListViewModel Get([FromUri]UsersSearchRequest searchRequest)
        {
            if (searchRequest == null || !ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }
            var response = usersService.GetAllUsers(searchRequest);
            return new UsersListViewModel
            {
                Data = response.Users.Select(x=>x.MapUserFromServerToClient()).ToList(),
                recordsFiltered = response.FilteredCount,
                recordsTotal = response.TotalCount
            };
        }
        #endregion
    }
}
