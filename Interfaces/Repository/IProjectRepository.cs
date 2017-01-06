using System.Collections.Generic;
using IST.Models.DomainModels;
using IST.Models.RequestModels;
using IST.Models.ResponseModels;

namespace IST.Interfaces.Repository
{
    /// <summary>
    /// IStudentRepository Repository
    /// </summary>
    public interface IProjectRepository : IBaseRepository<Project, long>
    {
        SearchTemplateResponse<Project> Search(ProjectSearchRequest searchRequest);
        //IEnumerable<Student> SearchByName(string name);
        Project GetById(int id);
    }
}
