using System;
using System.Collections.Generic;
using IST.Interfaces.IServices;
using IST.Interfaces.Repository;
using IST.Models.DomainModels;
using IST.Models.RequestModels;
using IST.Models.ResponseModels;

namespace IST.Implementation.Services
{
    /// <summary>
    /// QuantityScale Service
    /// </summary>
    public sealed class StudentService : IStudentService
    {
        #region Private
        private readonly IStudentRepository repository;
        private readonly IAllergyRepository allergyRepository;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public StudentService(IStudentRepository repository,
            IAllergyService allergyService, IAllergyRepository allergyRepository)
        {

            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            this.repository = repository;
            this.allergyRepository = allergyRepository;
        }

        #endregion

        #region Public
        

        public IEnumerable<Student> SearchByName( string name)
        {
            return repository.SearchByName(name);
        }

        /// <summary>
        /// Get Student By Id
        /// </summary>
        /// <param name="site"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Student GetById(int id)
        {
            return repository.GetById(id);
        }

        public bool Delete(int site)
        {
            var toDelete = repository.GetById(site);
            if (toDelete == null)
            {
                throw new Exception("Student may have already been deleted.");
            }
            toDelete.IsDeleted = true;
            repository.SaveChanges();
            return true;
        }

        public SearchTemplateResponse<Student> Search(StudentSearchRequest searchRequest)
        {
            return repository.Search(searchRequest);
        }

        #endregion


    }
}
