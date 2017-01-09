using System.Collections.Generic;
using IST.Models.DomainModels;
using IST.Models.RequestModels;
using IST.Models.ResponseModels;

namespace IST.Interfaces.Repository
{
    /// <summary>
    /// IStudentRepository Repository
    /// </summary>
    public interface ISolutionRepository : IBaseRepository<Solution, long>
    {
        SearchTemplateResponse<Solution> Search(SolutionSearchRequest searchRequest);
        //IEnumerable<Student> SearchByName(string name);
        Solution GetById(int id);
    }
}
