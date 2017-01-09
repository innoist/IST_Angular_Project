using System.Collections.Generic;
using IST.Interfaces.IServices;
using IST.Interfaces.Repository;
using IST.Models.DomainModels;

namespace IST.Implementation.Services
{
    public class SolutionOwnerService : ISolutionOwnerService
    {
        #region Private

        private readonly ISolutionOwnerRepository solutionOwnerRepository;

        #endregion

        #region Constructor

        public SolutionOwnerService(ISolutionOwnerRepository solutionOwnerRepository)
        {
            this.solutionOwnerRepository = solutionOwnerRepository;
        }

        #endregion

        #region Public

        public IEnumerable<SolutionOwner> GetAll()
        {
            return solutionOwnerRepository.GetAll();
        }

        public SolutionOwner FindSolutionOwnerById(int solutionOwnerId)
        {
            return solutionOwnerRepository.Find(solutionOwnerId);
        }

        public bool SaveSolutionOwner(SolutionOwner solutionOwner)
        {
            solutionOwnerRepository.Add(solutionOwner);
            solutionOwnerRepository.SaveChanges();
            return true;
        }

        public bool UpdateSolutionOwner(SolutionOwner solutionOwner)
        {
            solutionOwnerRepository.Update(solutionOwner);
            solutionOwnerRepository.SaveChanges();
            return true;
        }

        public bool DeleteSolutionOwner(int solutionOwnerId)
        {
            var toDelete = solutionOwnerRepository.Find(solutionOwnerId);
            solutionOwnerRepository.Delete(toDelete);
            solutionOwnerRepository.SaveChanges();
            return true;
        }

        #endregion
    }
}
