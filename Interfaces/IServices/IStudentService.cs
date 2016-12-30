using System.Collections.Generic;
using IST.Models.DomainModels;
using IST.Models.RequestModels;
using IST.Models.ResponseModels;

namespace IST.Interfaces.IServices
{
    /// <summary>
    /// Interface for Student Service
    /// </summary>
    public interface IStudentService
    {
        IEnumerable<Student> SearchByName(string name);
        Student GetById( int id);
        bool Delete(int id);
        SearchTemplateResponse<Student> Search(StudentSearchRequest searchRequest);
    }
}
