using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.QueueProcessor
{
    public class QueueMessage
    {
        public object MessageType { get; set; }
        public string TenantId { get; set; }
        public string AccessToken { get; set; }
        public string Message { get; set; }
    }
}
