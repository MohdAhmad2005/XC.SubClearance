using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.Workflow.Workflow.Models
{
    public class WorkflowTokenResponse
    {
        public string Token { get; set; }
        public string StatusCode { get; set; }
        public string Error { get; set; }
        public string ErrorDescription { get; set; }
    }

    public class WorkflowLoginSuccessResponse
    {
        public string Token { get; set; }
    }

    public class WorkflowLoginFailResponse
    {
        public string Message { get; set; }
    }
}
