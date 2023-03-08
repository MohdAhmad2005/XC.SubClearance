using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.ViewModels.TanentAction;

namespace XC.XSC.ViewModels.TenantActionDetail
{
    public class TenantActionResponse
    {
        /// <summary>
        /// Submission action response with constructor initailization
        /// </summary>
        public TenantActionResponse()
        {
            this.submissionStatusMappedAction = new List<SubmissionStatusMappedAction>();
            this.notAssignedSubmissionActions = new NotAssignedSubmissionActions();
        }
            
        /// <summary>
        /// Submission status with mapped Action List Detail
        /// </summary>
        public List<SubmissionStatusMappedAction> submissionStatusMappedAction { get; set; }
        /// <summary>

        /// <summary>
        /// not Assigned action 
        /// </summary>
        public NotAssignedSubmissionActions notAssignedSubmissionActions { get; set; }
    }
}
