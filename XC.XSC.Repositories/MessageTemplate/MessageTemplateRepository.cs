using MongoDB.Driver;
using System.Linq.Expressions;

namespace XC.XSC.Repositories.MessageTemplate
{
    /// <summary>
    /// This is the implementation class of IMessageTemplateRepository interface.
    /// </summary>
    public class MessageTemplateRepository : IMessageTemplateRepository
    {
        private readonly IMongoCollection<Models.Mongo.Entity.MessageTemplate.MessageTemplate> _mongoMessageTemplate;

        /// <summary>
        /// MessageTemplateRepository Constructor. 
        /// </summary>
        /// <param name="mongoDatabase">DBContext for mongo Db. </param>
        public MessageTemplateRepository(IMongoDatabase mongoDatabase)
        {
            _mongoMessageTemplate = mongoDatabase.GetCollection<Models.Mongo.Entity.MessageTemplate.MessageTemplate>("MessageTemplate");
        }

        /// <summary>
        /// Add MessageTemplate in MongoDb
        /// </summary>
        /// <param name="messageTemplate"></param>
        /// <returns></returns>
        public async Task<Models.Mongo.Entity.MessageTemplate.MessageTemplate> Add(Models.Mongo.Entity.MessageTemplate.MessageTemplate messageTemplate)
        {
            await _mongoMessageTemplate.InsertOneAsync(messageTemplate);

            return messageTemplate;
        }

        public Task AddAsync(Models.Mongo.Entity.MessageTemplate.MessageTemplate messageTemplate)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Expression<Func<Models.Mongo.Entity.MessageTemplate.MessageTemplate, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is the impplementation of GetAll() method get all the MessageTemplate documents from mongo.
        /// </summary>
        /// <returns>It returns a list of documents.</returns>
        public IQueryable<Models.Mongo.Entity.MessageTemplate.MessageTemplate> GetAll()
        {
            return _mongoMessageTemplate.AsQueryable();
        }

        /// <summary>
        /// This method will get single MessageTemplate document from mongo.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>It returns a list of documents.</returns>
        public async Task<Models.Mongo.Entity.MessageTemplate.MessageTemplate> GetSingleAsync(Expression<Func<Models.Mongo.Entity.MessageTemplate.MessageTemplate, bool>> predicate)
        {
            var filter = Builders< Models.Mongo.Entity.MessageTemplate.MessageTemplate>.Filter.Where(predicate);

            return (await _mongoMessageTemplate.FindAsync(filter)).FirstOrDefault();
        }

        /// <summary>
        /// This method will be used to update a document in mongo.
        /// </summary>
        /// <param name="messageTemplate"></param>
        /// <returns></returns>
        public async Task<Models.Mongo.Entity.MessageTemplate.MessageTemplate> UpdateAsync(Models.Mongo.Entity.MessageTemplate.MessageTemplate messageTemplate)
        {
            return await _mongoMessageTemplate.FindOneAndReplaceAsync(x => x.Id == messageTemplate.Id, messageTemplate);
        }
    }
}
