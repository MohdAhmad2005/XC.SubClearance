using XC.XSC.ViewModels.SubmissionExtraction;

namespace XC.XSC.Models.Mongo.Interface
{
    public interface ISubmissionExtraction
    {
        /// <summary>
        /// SubmissionId property for ISubmissionExtraction.
        /// </summary>
        string SubmissionId { get; set; }

        /// <summary>
        /// SubmissionFormData property of type SubmissionFormData.
        /// </summary>
        SubmissionFormData SubmissionFormData { get; set; }
        /// <summary>
        /// CreatedBy property for ISubmissionExtraction.
        /// </summary>
         string CreatedBy { get; set; }

        /// <summary>
        /// CreatedOn property for ISubmissionExtraction.
        /// </summary>
         DateTime CreatedOn { get; set; }

        /// <summary>
        /// ModifiedBy property for ISubmissionExtraction.
        /// </summary>
         string ModifiedBy { get; set; } 

        /// <summary>
        /// ModifiedOn property for ISubmissionExtraction.
        /// </summary>
         DateTime ModifiedOn { get; set; }

        /// <summary>
        /// IsActive property for ISubmissionExtraction.
        /// </summary>
         bool IsActive { get; set; }
    }
}
