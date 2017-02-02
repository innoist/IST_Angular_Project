using System;
using System.Security.Claims;
using IST.Interfaces.IServices;
using IST.Interfaces.Repository;
using IST.Models.DomainModels;
using Microsoft.AspNet.Identity;

namespace IST.Implementation.Services
{
    public class UsageHistoryService : IUsageHistoryService
    {
        private readonly IUsageHistoryRepository usageHistoryRepository;

        public UsageHistoryService(IUsageHistoryRepository usageHistoryRepository)
        {
            this.usageHistoryRepository = usageHistoryRepository;
        }

        public bool SaveUsage(int solutionId, int usageType)
        {
            SolutionUsageHistory usageToSave = new SolutionUsageHistory
            {
                SolutionId = solutionId,
                UsageType = usageType,
                UsageTime = DateTime.Now,
                RecCreatedById = ClaimsPrincipal.Current.Identity.GetUserId(),
                RecCreatedOn = DateTime.Now,
                RecLastUpdatedById = ClaimsPrincipal.Current.Identity.GetUserId(),
                RecLastUpdatedOn = DateTime.Now
            };
            usageHistoryRepository.Add(usageToSave);
            usageHistoryRepository.SaveChanges();
            return true;
        }
    }
}
