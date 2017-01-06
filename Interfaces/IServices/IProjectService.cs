using System.Collections.Generic;
using IST.Models.DomainModels;
using IST.Models.RequestModels;
using IST.Models.ResponseModels;

namespace IST.Interfaces.IServices
{
    /// <summary>
    /// Interface for Student Service
    /// </summary>
    public interface IProjectService
    {
        bool Delete(int id);
        SearchTemplateResponse<Project> Search(ProjectSearchRequest searchRequest);
        Project GetById(int id);
    }
}
