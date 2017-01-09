using System.Collections.Generic;
using IST.Interfaces.IServices;
using IST.Interfaces.Repository;
using IST.Models.DomainModels;

namespace IST.Implementation.Services
{
    public class SolutionTypeService : ISolutionTypeService
    {
        #region Private

        private readonly ISolutionTypeRepository solutionTypeRepository;

        #endregion

        #region Constructor

        public SolutionTypeService(ISolutionTypeRepository solutionTypeRepository)
        {
            this.solutionTypeRepository = solutionTypeRepository;
        }

        #endregion

        #region Public

        public IEnumerable<SolutionType> GetAll()
        {
            return solutionTypeRepository.GetAll();
        }

        public SolutionType FindSolutionTypeById(int solutionTypeId)
        {
            return solutionTypeRepository.Find(solutionTypeId);
        }

        public bool SaveSolutionType(SolutionType solutionType)
        {
            solutionTypeRepository.Add(solutionType);
            solutionTypeRepository.SaveChanges();
            return true;
        }

        public bool UpdateSolutionType(SolutionType solutionType)
        {
            solutionTypeRepository.Update(solutionType);
            solutionTypeRepository.SaveChanges();
            return true;
        }

        public bool DeleteSolutionType(int solutionTypeId)
        {
            var toDelete = solutionTypeRepository.Find(solutionTypeId);
            solutionTypeRepository.Delete(toDelete);
            solutionTypeRepository.SaveChanges();
            return true;
        }

        #endregion
    }
}
