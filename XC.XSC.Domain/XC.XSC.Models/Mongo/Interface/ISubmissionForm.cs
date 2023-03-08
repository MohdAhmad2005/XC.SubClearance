using XC.XSC.ViewModels.SubmissionExtraction;

namespace XC.XSC.Models.Mongo.Interface
{
    public interface ISubmissionForm
    {
        /// <summary>
        /// SubmissionEditForm property of type SubmissionEditForm.
        /// </summary>
        SubmissionEditForm SubmissionEditForm { get; set; }
        /// <summary>
        /// CreatedBy property for ISubmissionForm.
        /// </summary>
         string CreatedBy { get; set; }

        /// <summary>
        /// CreatedOn property for ISubmissionForm.
        /// </summary>
         DateTime CreatedOn { get; set; }

        /// <summary>
        /// ModifiedBy property for ISubmissionForm.
        /// </summary>
         string ModifiedBy { get; set; } 

        /// <summary>
        /// ModifiedOn property for ISubmissionForm.
        /// </summary>
         DateTime ModifiedOn { get; set; }

        /// <summary>
        /// IsActive property for ISubmissionForm.
        /// </summary>
         bool IsActive { get; set; }

        /// <summary>
        /// TenantId property for ISubmissionForm.
        /// </summary>
        string TenantId { get; set; }
    }
}
