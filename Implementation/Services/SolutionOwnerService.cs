using System.Collections.Generic;
using IST.Interfaces.IServices;
using IST.Interfaces.Repository;
using IST.Models.DomainModels;
using IST.Repository.BaseRepository;

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
            return solutionOwnerRepository.GetAllSolutionOwners();
        }

        public SolutionOwner FindSolutionOwnerById(int solutionOwnerId)
        {
            return solutionOwnerRepository.FindSolutionOwnerById(solutionOwnerId);
        }

        public bool SaveOrUpdate(SolutionOwner solutionOwner)
        {
            if (solutionOwner.Id > 0)
            {
                var recordToUpdate = solutionOwnerRepository.Find(solutionOwner.Id);
                recordToUpdate.DisplayValue = solutionOwner.DisplayValue;
                recordToUpdate.RecLastUpdatedById = solutionOwner.RecLastUpdatedById;
                recordToUpdate.RecLastUpdatedOn = solutionOwner.RecLastUpdatedOn;
            }
            else
            {
                solutionOwnerRepository.Add(solutionOwner);
            }
            solutionOwnerRepository.SaveChanges();
            return true;
        }
        
        public bool RemoveSolutionOwner(int solutionOwnerId)
        {
            BaseDbContext context = new BaseDbContext();
            context.RemoveSolutionOwner(solutionOwnerId);
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
