using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.SubmissionAuditLog
{
    public class SubmissionCloneData
    {
        /// <summary>
        /// this is used for Previous status id
        /// </summary>
        public int PrevStatusId { get; set; }

        /// <summary>
        /// this is used for Previous Assisgned id
        /// </summary>
        public string PrevAssignedId { get; set; }

        /// <summary>
        /// this is use for clone the data 
        /// </summary>
        public SubmissionCloneData()
        {
        }

        /// <summary>
        /// this is used for clone the data for previous status
        /// </summary>
        /// <param name="submissionCloneData"></param>
        public SubmissionCloneData(SubmissionCloneData submissionCloneData)
        {
            PrevStatusId = submissionCloneData.PrevStatusId;
            PrevAssignedId = submissionCloneData.PrevAssignedId;
        }
    }
}
