using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.Models.Interface.TaskAuditHistory
{
    public interface ITaskAuditHistory
    {

        public long SubmissionId { get; set; }
        public int SubmissionStatusId { get; set; }
        public int SubmissionStageId { get; set; }
        public bool IsAcrchieved { get; set; }
        public string StageData { get; set; }
        public string UserId { get; set; }


    }
}
