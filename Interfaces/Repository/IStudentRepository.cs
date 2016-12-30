using System.Collections.Generic;
using IST.Models.DomainModels;
using IST.Models.RequestModels;
using IST.Models.ResponseModels;

namespace IST.Interfaces.Repository
{
    /// <summary>
    /// IStudentRepository Repository
    /// </summary>
    public interface IStudentRepository : IBaseRepository<Student, long>
    {
        SearchTemplateResponse<Student> Search(StudentSearchRequest searchRequest);
        IEnumerable<Student> SearchByName(string name);
        Student GetById(int id);
        
    }
}
