namespace XC.XSC.Repositories.MessageTemplate
{
    public interface IMessageTemplateRepository : IRepository<Models.Mongo.Entity.MessageTemplate.MessageTemplate>
    {
        /// <summary>
        /// Add MessageTemplate in MongoDb
        /// </summary>
        /// <param name="messageTemplate"></param>
        /// <returns></returns>
        Task<Models.Mongo.Entity.MessageTemplate.MessageTemplate> Add(Models.Mongo.Entity.MessageTemplate.MessageTemplate messageTemplate);
    }
}
