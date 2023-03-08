using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using XC.XSC.Models.Mongo.Interface.MessageTemplate;
using XC.XSC.ViewModels.Enum;

namespace XC.XSC.Models.Mongo.Entity.MessageTemplate
{
    public class MessageTemplate :BaseEntity, IMessageTemplate
    {
        /// <summary>
        /// Id property for MessageTemplate document.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets MessageType
        /// </summary>
        public MessageType MsgType { get; set; }

        /// <summary>
        /// Gets or sets Key
        /// </summary>
        public string TemplateKey { get; set; } = string.Empty;

        /// <summary>
        /// Used in Mongo DB Mail
        /// </summary>
        public Mails Mails { get; set; }

        /// <summary>
        /// Used in MongoDb Notification
        /// </summary>
        public Notifications Notifications { get; set; }

    }

    public class Mails
    {
        /// <summary>
        /// Gets or sets Mail Subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets Mail Template Body
        /// </summary>
        public string TemplateBody { get; set; }
    }

    public class Notifications
    {
        /// <summary>
        /// Gets or sets Mail Subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets Mail Template Body
        /// </summary>
        public string TemplateBody { get; set; }
    }
}
