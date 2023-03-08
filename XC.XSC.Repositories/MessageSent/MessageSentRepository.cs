using MongoDB.Driver;
using System.Linq.Expressions;

namespace XC.XSC.Repositories.MessageSent
{
    /// <summary>
    /// This is the implementation class of ISubmissionExtractionRepository interface.
    /// </summary>
    public class MessageSentRepository : IMessageSentRepository
    {
        private readonly IMongoCollection<Models.Mongo.Entity.MessageSent.MessageSent> _mongoMessageSent;

        /// <summary>
        /// MessageSentRepository Constructor. 
        /// </summary>
        /// <param name="mongoDatabase">DBContext for mongo Db. </param>
        public MessageSentRepository(IMongoDatabase mongoDatabase)
        {
            _mongoMessageSent = mongoDatabase.GetCollection<Models.Mongo.Entity.MessageSent.MessageSent>("MessageSent");
        }

        /// <summary>
        /// Add Message Sent
        /// </summary>
        /// <param name="MessageSent"></param>
        /// <returns></returns>
        public async Task<Models.Mongo.Entity.MessageSent.MessageSent> Add(Models.Mongo.Entity.MessageSent.MessageSent MessageSent)
        {
            await _mongoMessageSent.InsertOneAsync(MessageSent);

            return MessageSent;
        }

        public Task AddAsync(Models.Mongo.Entity.MessageSent.MessageSent MessageSent)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Expression<Func<Models.Mongo.Entity.MessageSent.MessageSent, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is the impplementation of GetAll() method get all the MessageSent documents from mongo.
        /// </summary>
        /// <returns>It returns a list of documents.</returns>
        public IQueryable<Models.Mongo.Entity.MessageSent.MessageSent> GetAll()
        {
            return _mongoMessageSent.AsQueryable();
        }

        /// <summary>
        /// This method will get single MessageSent document from mongo.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>It returns a list of documents.</returns>
        public async Task<Models.Mongo.Entity.MessageSent.MessageSent> GetSingleAsync(Expression<Func<Models.Mongo.Entity.MessageSent.MessageSent, bool>> predicate)
        {
            var filter = Builders<Models.Mongo.Entity.MessageSent.MessageSent>.Filter.Where(predicate);

            return (await _mongoMessageSent.FindAsync(filter)).FirstOrDefault();
        }

        /// <summary>
        /// This method will be used to update a document in mongo.
        /// </summary>
        /// <param name="MessageSent"></param>
        /// <returns></returns>
        public async Task<Models.Mongo.Entity.MessageSent.MessageSent> UpdateAsync(Models.Mongo.Entity.MessageSent.MessageSent MessageSent)
        {
            return await _mongoMessageSent.FindOneAndReplaceAsync(x => x.Id == MessageSent.Id, MessageSent);
        }
    }
}
