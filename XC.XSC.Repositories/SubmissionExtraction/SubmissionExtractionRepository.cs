using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.Data;

namespace XC.XSC.Repositories.SubmissionExtraction
{
    /// <summary>
    /// This is the implementation class of ISubmissionExtractionRepository interface.
    /// </summary>
    public class SubmissionExtractionRepository: ISubmissionExtractionRepository
    {
        private readonly IMongoCollection<Models.Mongo.Entity.SubmissionExtraction> _mongoConfiguration;
        private readonly IMongoCollection<Models.Mongo.Entity.SubmissionForm> _mongoSubmissionFormConfiguration;

        /// <summary>
        /// SubmissionExtractionRepository Constructor. 
        /// </summary>
        /// <param name="mongoDatabase">DBContext for mongo Db. </param>
        public SubmissionExtractionRepository(IMongoDatabase mongoDatabase)
        {
            _mongoConfiguration = mongoDatabase.GetCollection<Models.Mongo.Entity.SubmissionExtraction>("Submissions");
            _mongoSubmissionFormConfiguration = mongoDatabase.GetCollection<Models.Mongo.Entity.SubmissionForm>("SubmissionForms");
        }

        /// <summary>
        /// Adds submission extraction JSON into mongo.
        /// </summary>
        /// <param name="submissionExtraction"> object of submission extraction.</param>

        public async Task<Models.Mongo.Entity.SubmissionExtraction> Add(Models.Mongo.Entity.SubmissionExtraction submissionExtraction)
        {
            await _mongoConfiguration.InsertOneAsync(submissionExtraction);

            return submissionExtraction;
        }

        /// <summary>
        /// Method used for add records in mongo.
        /// </summary>
        /// <param name="obj">object of submission extraction.</param>
        /// <exception cref="NotImplementedException"></exception>
        public Task AddAsync(Models.Mongo.Entity.SubmissionExtraction obj)
        {
            throw new NotImplementedException();

        }

        /// <summary>
        /// Delete from mongo configurations.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task DeleteAsync(Expression<Func<Models.Mongo.Entity.SubmissionExtraction, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is the impplementation of GetAll() method get all the extraction documents from mongo.
        /// </summary>
        /// <returns>It returns a list of documents.</returns>
        public IQueryable<Models.Mongo.Entity.SubmissionExtraction> GetAll()
        {
            return _mongoConfiguration.AsQueryable();
        }

        /// <summary>
        /// This method will get single extraction document from mongo.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>It returns a list of documents.</returns>
        public async Task<Models.Mongo.Entity.SubmissionExtraction> GetSingleAsync(Expression<Func<Models.Mongo.Entity.SubmissionExtraction, bool>> predicate)
        {
            var filter = Builders<Models.Mongo.Entity.SubmissionExtraction>.Filter.Where(predicate);

            return (await _mongoConfiguration.FindAsync(filter)).FirstOrDefault();
        }

        /// <summary>
        /// This method will be used to update a document in mongo.
        /// </summary>
        /// <param name="obj"> object of type SubmissionExtraction.</param>
        /// <returns> Updated document Of type SubmissionExtraction.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Models.Mongo.Entity.SubmissionExtraction> UpdateAsync(Models.Mongo.Entity.SubmissionExtraction obj)
        {
            return await _mongoConfiguration.FindOneAndReplaceAsync(x => x.Id == obj.Id, obj);
        }

        /// <summary>
        /// This method will get single extraction document from mongo.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>It returns a document.</returns>
        public async Task<Models.Mongo.Entity.SubmissionExtraction> GetSingleAsync(FilterDefinition<Models.Mongo.Entity.SubmissionExtraction> filter)
        {
            return (await _mongoConfiguration.FindAsync(filter)).FirstOrDefault();
        }


        /// <summary>
        /// This method will get submission form from mongo.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>It returns a document.</returns>
        public async Task<Models.Mongo.Entity.SubmissionForm> GetSubmissionForm(FilterDefinition<Models.Mongo.Entity.SubmissionForm> filter)
        {
            return (await _mongoSubmissionFormConfiguration.FindAsync(filter)).FirstOrDefault();
        }
    }
}
