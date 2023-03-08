using XC.XSC.Models.Mongo.Entity.MessageTemplate;
using XC.XSC.ViewModels.Enum;

namespace XC.XSC.Models.Mongo.Interface.MessageTemplate
{
    public interface IMessageTemplate
    {
        /// <summary>
        /// Gets or sets MessageType
        /// </summary>
       public MessageType MsgType { get; set; }

        /// <summary>
        /// Gets or sets Key
        /// </summary>
        string TemplateKey { get; set; }

        /// <summary>
        /// Used in Mongo DB Mail
        /// </summary>
        Mails Mails { get; set; }

        /// <summary>
        /// Used in MongoDb Notification
        /// </summary>
        Notifications Notifications { get; set; }
    }
}
