using System.Collections.Generic;
using IST.Interfaces.IServices;
using IST.Interfaces.Repository;
using IST.Models.DomainModels;
using IST.Repository.BaseRepository;

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
            return solutionTypeRepository.GetAllSolutionTypes();
        }

        public SolutionType FindSolutionTypeById(int solutionTypeId)
        {
            return solutionTypeRepository.FindSolutionTypeById(solutionTypeId);
        }

        public bool SaveOrUpdate(SolutionType solutionType)
        {
            if (solutionType.Id > 0)
            {
                var recordToUpdate = solutionTypeRepository.Find(solutionType.Id);
                recordToUpdate.DisplayValue = solutionType.DisplayValue;
                recordToUpdate.RecLastUpdatedById = solutionType.RecLastUpdatedById;
                recordToUpdate.RecLastUpdatedOn = solutionType.RecLastUpdatedOn;
            }
            else
            {
                solutionTypeRepository.Add(solutionType);
            }
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
            BaseDbContext context = new BaseDbContext();
            context.DeleteSolutionType(solutionTypeId);
            return true;
        }

        #endregion
    }
}
