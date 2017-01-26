using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using IST.Interfaces.IServices;
using IST.Interfaces.Repository;
using IST.Models.Common.DropDown;
using IST.Models.DomainModels;
using IST.Models.RequestModels;
using IST.Models.ResponseModels;
using Microsoft.AspNet.Identity;

namespace IST.Implementation.Services
{
    /// <summary>
    /// QuantityScale Service
    /// </summary>
    public sealed class SolutionRatingService : ISolutionRatingService
    {
        #region Private

        private readonly ISolutionRepository solutionRepository;
        private readonly ISolutionRatingRepository solutionRatingRepository;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SolutionRatingService(ISolutionRepository solutionRepository, ISolutionRatingRepository solutionRatingRepository)
        {
            this.solutionRepository = solutionRepository;
            this.solutionRatingRepository = solutionRatingRepository;
        }

        #endregion

        #region Public

        public SearchTemplateResponse<Solution> Search(SolutionSearchRequest searchRequest)
        {
            return solutionRepository.Search(searchRequest);
        }

        public Solution GetById(int id)
        {
            return solutionRepository.GetById(id);
        }
        
        public bool SaveOrUpdate(SolutionRating model)
        {
            if (model.RatingId > 0)
            {
                var toUpdate = solutionRatingRepository.Find(model.RatingId);
                toUpdate.SolutionId = model.SolutionId;
                toUpdate.Rating = model.Rating;
                toUpdate.Comments = model.Comments;
                toUpdate.RecLastUpdatedById = model.RecLastUpdatedById;
                toUpdate.RecLastUpdatedOn = model.RecLastUpdatedOn;
                solutionRatingRepository.Update(toUpdate);

            }
            else
            {
                solutionRatingRepository.Add(model);
            }
            solutionRatingRepository.SaveChanges();
            return true;
        }
        
        #endregion

    }
}