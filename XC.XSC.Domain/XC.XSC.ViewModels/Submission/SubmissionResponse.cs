using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.ViewModels.QueueProcessor;

namespace XC.XSC.ViewModels.Submission
{
    public class SubmissionResponse
    {
        /// <summary>
        /// /SubmissionID
        /// </summary>
        public long SubmissionID { get; set; }

        /// <summary>
        /// CaseNumber
        /// </summary>
        public string CaseNumber { get; set; } = string.Empty;

        /// <summary>
        /// BrokerName
        /// </summary>
        public string BrokerName { get; set; } = string.Empty;

        /// <summary>
        /// InsuredName
        /// </summary>
        public string InsuredName { get; set; } = string.Empty;

        /// <summary>
        /// FromEmail
        /// </summary>
        public string FromEmail { get; set; } = string.Empty;

        /// <summary>
        /// RecieveDate
        /// </summary>
        public DateTime RecieveDate { get; set; }

        /// <summary>
        /// DueDate
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// StatusId
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// StatusName
        /// </summary>
        public string StatusName { get; set; } = string.Empty;

        /// <summary>
        /// IsInScope
        /// </summary>
        public bool IsInScope { get; set; }

        /// <summary>
        /// StatusLabel
        /// </summary>
        public string StatusLabel { get; set; } = string.Empty;

        /// <summary>
        /// StatusColor
        /// </summary>
        public string StatusColor { get; set; } = string.Empty;

        /// <summary>
        /// StageId
        /// </summary>
        public int StageId { get; set; }

        /// <summary>
        /// StageName
        /// </summary>
        public string StageName { get; set; } = string.Empty;

        /// <summary>
        /// StageLabel
        /// </summary>
        public string StageLabel { get; set; } = string.Empty;

        /// <summary>
        /// StageColor
        /// </summary>
        public string StageColor { get; set; } = string.Empty;

        /// <summary>
        /// AssignedTo as Userid
        /// </summary>
        public string? AssignedTo { get; set; } = string.Empty;

        /// <summary>
        /// Submission Comments
        /// </summary>
        public string? Comments { get; set; } = string.Empty;

        /// <summary>
        /// Email Info Id.
        /// </summary>
        public long EmailInfoId { get; set; }

        /// <summary>
        /// Lob Name.
        /// </summary>
        public string? LobName { get; set; }

        /// <summary>
        /// Lob Id.
        /// </summary>
        public int LobId { get; set; }

        /// <summary>
        /// Team Id.
        /// </summary>
        public int TeamId { get; set; }

        /// <summary>
        /// RegionId Id.
        /// </summary>
        public int RegionId { get; set; }

        /// <summary>
        /// Clearance consent.
        /// </summary>
        public bool ClearanceConsent { get; set; }

        /// <summary>
        /// Is data completed.
        /// </summary>
        public bool isDataCompleted { get; set; }
    }
}
