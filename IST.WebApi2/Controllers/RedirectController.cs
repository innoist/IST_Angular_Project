using System.Web.Http;
using RedirectResult = System.Web.Http.Results.RedirectResult;

namespace IST.WebApi2.Controllers
{
    public class RedirectController : BaseController
    {
        [AllowAnonymous]
        public IHttpActionResult Get(string userId, string email, int solutionId)
        {
            string url = "http://www.google.com";
            System.Uri uri = new System.Uri(url);
            return Redirect(uri);
        }
    }
}