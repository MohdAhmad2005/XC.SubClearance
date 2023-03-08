using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.Workflow.Workflow.Models
{
    public class StartWorkflowRequest
    {
        public string BusinessKey { get; set; }
        public Variables Variables { get; set; }
    }
    public class Variables
    {
        public string TenantId { get; set; }
        public string ThresholdLimit { get; set; }
        public string SubmissionId { get; set; }
        public string LobId { get; set; }
        public string TeamId { get; set; }
        public string ConfigurationId { get; set; }
        public string MessageId { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string WorkFlowId { get; set; }
    }

}
