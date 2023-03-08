using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XC.XSC.Workflow.Workflow.Models
{
    public class WorkflowLoginRequest
    {
        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("userName")]
        public string UserName { get; set; }

       
    }
}
