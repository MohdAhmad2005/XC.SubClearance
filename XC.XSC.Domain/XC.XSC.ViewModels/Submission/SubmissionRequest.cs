using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.Submission
{
    /// <summary>
    /// This class used to get the submission request
    /// </summary>
    public class SubmissionRequest
    {
        /// <summary>
        /// Id of the submission
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Case id of the submission
        /// </summary>
        public string CaseId { get; set; }

        /// <summary>
        /// Broker name of the submission
        /// </summary>
        public string BrokerName { get; set; }

        /// <summary>
        /// Insured name of the submission
        /// </summary>
        public string InsuredName { get; set; }

        /// <summary>
        /// Email info id of the submission
        /// </summary>
        public long EmailInfoId { get; set; }

        /// <summary>
        /// Due date of the submission
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Owner of the submission
        /// </summary>
        public string AssignedId { get; set; }

        /// <summary>
        /// Submission staus id of the submission
        /// </summary>
        public int SubmissionStatusId { get; set; }

        /// <summary>
        /// Submission stage id of the submission
        /// </summary>
        public int SubmissionStageId { get; set; }

        /// <summary>
        /// Whether the submission is in scope 
        /// </summary>
        public bool IsInScope { get; set; }

        /// <summary>
        /// Lob id of the submission
        /// </summary>
        public int LobId { get; set; }

        /// <summary>
        /// Extended date of the submission
        /// </summary>
        public DateTime ExtendedDate { get; set; }

        /// <summary>
        /// Email body of the submission
        /// </summary>
        public string EmailBody { get; set; }

        /// <summary>
        /// Task id of the submission
        /// </summary>
        public string TaskId { get; set; }

        /// <summary>
        /// Tenant id of the submission
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// Created date of the submission
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Who has created the submission
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Modified date of thesubmission
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Who lastly modified the submission
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Whether the submission is in active state or not
        /// </summary>
        public bool IsActive { get; set; }
    }
}
