namespace XC.XSC.Repositories.MessageSent
{
    public interface IMessageSentRepository : IRepository<Models.Mongo.Entity.MessageSent.MessageSent>
    {
        /// <summary>
        /// Add MessageSent in MongoDb
        /// </summary>
        /// <param name="messageSent"></param>
        /// <returns></returns>
        Task<Models.Mongo.Entity.MessageSent.MessageSent> Add(Models.Mongo.Entity.MessageSent.MessageSent messageSent);
    }
}
