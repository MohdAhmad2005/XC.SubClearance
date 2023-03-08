using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.ViewModels.Enum;

namespace XC.XSC.ViewModels.Log.Response
{
    public class ActivityLogResponse
    {
        public string Id { get; set; }
        public string TenantId { get; set; }
        public LogType LogType { get; set; }
        public object Data { get; set; }
        public DateTime ActivityOn { get; set; }
        public string ActivityBy { get; set; }
    }
}
