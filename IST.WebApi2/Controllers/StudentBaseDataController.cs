using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using IST.Interfaces.IServices;
using IST.Models.Common.DropDown;

namespace IST.WebApi2.Controllers
{   
    public class StudentBaseDataController : BaseController
    {
        #region Private
        private readonly IStudentService studentService;
        #endregion

        #region Constructor
        public StudentBaseDataController(IStudentService studentService)
        {
            this.studentService = studentService;
        }
        #endregion

        #region Public
        
        #endregion
    }
}
