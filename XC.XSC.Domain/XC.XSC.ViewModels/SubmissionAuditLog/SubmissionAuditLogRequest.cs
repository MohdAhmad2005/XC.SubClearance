using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.SubmissionAuditLog
{
    public class SubmissionAuditLogRequest
    {
        /// <summary>
        /// It is date Data in SubmissionId
        /// </summary>
        public long SubmissionId { get; set; }

        /// <summary>
        /// It is date Data in PrevStatus
        /// </summary>
        public int PrevStatus { get; set; }

        /// <summary>
        /// It is by Data in NewStatus 
        /// </summary>
        public int NewStatus { get; set; }

        /// <summary>
        /// It is by Data in PrevAssignedToId 
        /// </summary>
        public string PrevAssignedToId { get; set; }

        /// <summary>
        /// It is by Data in NewAssignedToId 
        /// </summary>
        public string NewAssignedToId { get; set; }
    }
}
