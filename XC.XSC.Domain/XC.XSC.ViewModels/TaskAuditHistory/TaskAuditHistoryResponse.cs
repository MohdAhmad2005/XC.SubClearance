using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.ViewModels.Submission;

namespace XC.XSC.ViewModels.TaskAuditHistory
{
    /// <summary>
    /// Task auditHistory Response model.
    /// </summary>
    public class TaskAuditHistoryResponse
    {
        /// <summary>
        /// It is list of Task Stage Data field.
        /// </summary>
        public List<TaskSatgeData> StageData { get; set; }

    }
    public class TaskSatgeData
    {
        /// <summary>
        /// It is Action Data in Stage Data 
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// It is date Data in Stage Data 
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// It is by Data in Stage Data 
        /// </summary>
        public string By { get; set; }  

    }
   
    public class TaskAuditHistoryWithDurationResponse
    {
        /// <summary>
        /// It is Total Days In Processing  field.
        /// </summary>
        public int ProcessingDays { get; set; }

        /// <summary>
        /// It is Total Hours In Processing  field.
        /// </summary>
        public int ProcessingHour { get; set; }

        /// <summary>
        /// It is Total Min In Processing  field.
        /// </summary>
        public int ProcessingMins { get; set; }

        /// <summary>
        /// It is Total Seceonds In Processing  field.
        /// </summary>
        public int ProcessingSecs { get; set; }

        /// <summary>
        /// It is Total Days In Review  field.
        /// </summary>
        public int ReviewDays { get; set; }

        /// <summary>
        /// It is Total Hours In Review  field.
        /// </summary>
        public int ReviewHour { get; set; }

        /// <summary>
        /// It is Total Min In Review  field.
        /// </summary>
        public int ReviewMins { get; set; }

        /// <summary>
        /// It is Total Second In Review  field.
        /// </summary>
        public int ReviewSecs { get; set; }

        /// <summary>
        /// It is Total Days In Review  field.
        /// </summary>
        public int QueryDays { get; set; }

        /// <summary>
        /// It is Total Days In Query  field.
        /// </summary>
        public int QueryHour { get; set; }

        /// <summary>
        /// It is Total Days In Query  field.
        /// </summary>
        public int QueryMins { get; set; }

        /// <summary>
        /// It is Total Days In Query  field.
        /// </summary>
        public int QuerySecs { get; set; }

        /// <summary>
        /// It is Total Days    field.
        /// </summary>
        public int TotalDay { get; set; }

        /// <summary>
        /// It is Total Hour In Processing  field.
        /// </summary>
        public int TotalHour { get; set; }

        /// <summary>
        /// It is Total Mins In Processing  field.
        /// </summary>
        public int TotalMins { get; set; }

        /// <summary>
        /// It is Total Secs In Processing  field.
        /// </summary>
        public int TotalSecs { get; set; }

    }

}
