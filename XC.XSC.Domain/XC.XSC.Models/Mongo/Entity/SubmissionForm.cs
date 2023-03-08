using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using XC.XSC.Models.Mongo.Interface;
using XC.XSC.ViewModels.SubmissionExtraction;

namespace XC.XSC.Models.Mongo.Entity
{
    public class SubmissionForm : ISubmissionForm
    {
        /// <summary>
        /// Id property for SubmissionExtraction document.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// SubmissionFormData property of type SubmissionFormData.
        /// </summary>
        public SubmissionEditForm SubmissionEditForm { get; set; }

        /// <summary>
        /// CreatedBy property for SubmissionExtraction.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// CreatedOn property for SubmissionExtraction.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// ModifiedBy property for SubmissionExtraction.
        /// </summary>
        public string ModifiedBy { get; set; } = string.Empty;

        /// <summary>
        /// ModifiedOn property for SubmissionExtraction.
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// IsActive property for SubmissionExtraction.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// TenantId property for ISubmissionForm.
        /// </summary>
        public string TenantId { get; set; }
    }
}
