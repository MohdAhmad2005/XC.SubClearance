using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.Models.Interface.Submission
{
    /// <summary>
    /// This model is used to return the submission table data
    /// </summary>
    public interface ISubmission
    {
        /// <summary>
        /// Case Id of the corresponding submission
        /// </summary>
        public string CaseId { get; set; }

        /// <summary>
        /// Broker name of the corresponding submission
        /// </summary>
        public string BrokerName { get; set; }

        /// <summary>
        /// Insurer name of the corresponding submission
        /// </summary>
        public string InsuredName { get; set; }

        /// <summary>
        /// Email info id of the corresponding submission
        /// </summary>
        public long EmailInfoId { get; set; }

        /// <summary>
        /// Due date of the corresponding submission
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Owner of corresponding submission
        /// </summary>
        public string AssignedId { get; set; }

        /// <summary>
        /// Submission status of the corresponding submission
        /// </summary>
        public int SubmissionStatusId { get; set; }

        /// <summary>
        /// Submission stage id of the corresponding submission
        /// </summary>
        public int SubmissionStageId { get; set; }

        /// <summary>
        /// To check whether the submission is in scope
        /// </summary>
        public bool IsInScope { get; set; }

        /// <summary>
        /// Lob id of the corresponding submission
        /// </summary>
        public int LobId { get; set; }

        /// <summary>
        /// Extended date of the corresponding submission
        /// </summary>
        public DateTime ExtendedDate { get; set; }

        /// <summary>
        /// Email body of the corresponding submission
        /// </summary>
        public string EmailBody { get; set; }

        /// <summary>
        /// Task id of the corresponding submission
        /// </summary>
        public string TaskId { get; set; }

        /// <summary>
        /// To check whether the submission is in ClearanceConscent
        /// </summary>
        public bool ClearanceConscent { get; set; }

        /// <summary>
        /// User TeamId
        /// </summary>
        public int TeamId { get; set; }

        /// <summary>
        /// User RegionId
        /// </summary>
        public int RegionId { get; set; }

        /// <summary>
        /// Gets or Sets the CompletionDate
        /// </summary>
        public DateTime? CompletionDate { get; set; }

        /// <summary>
        /// Gets or Sets the DataReferenceId
        /// </summary>
        public Guid? DataReferenceId { get; set; }

        /// <summary>
        /// Reviewer id field.
        /// </summary>
        public string? ReviewerId { get; set; }

        /// <summary>
        /// Is data completed.
        /// </summary>
        public bool isDataCompleted { get; set; }
    }
}
