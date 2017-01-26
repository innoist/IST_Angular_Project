using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using IST.Interfaces.Repository;
using IST.Models.DomainModels;
using IST.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace IST.Repository.Repositories
{
    public class SolutionRatingRepository : BaseRepository<SolutionRating>, ISolutionRatingRepository
    {
        public SolutionRatingRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<SolutionRating> DbSet => db.SolutionRatings;

        public IEnumerable<SolutionRating> GetRatingBySolutionId(int id)
        {
            var query = DbSet.Where(s => s.SolutionId == id).ToList();
            return query;
        }
    }
}
