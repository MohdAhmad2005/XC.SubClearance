using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using XC.XSC.Models.Interface.Log;
using XC.XSC.ViewModels.Enum;

namespace XC.XSC.Models.Entity.Log
{
    public class ActivityLog : IActivityLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string TenantId { get; set; }
        public LogType LogType { get; set; }
        public object Data { get; set; }
        public DateTime ActivityOn { get; set; }
        public string ActivityBy { get; set; }
    }
}
