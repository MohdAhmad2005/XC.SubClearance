using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.Submission
{
    /// <summary>
    /// This view model is written to get the performance related information.
    /// </summary>
    public class PerformanceResponse
    {
        /// <summary>
        /// This field stores AssignedDate of submission.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// This field stores Assigned ProcessorName.
        /// </summary>
        public String ProcessorName { get; set; }

        /// <summary>
        /// This field stores total count of cases that is assigned on a particular date.
        /// </summary>
        public int AssignedCount { get; set; }

        /// <summary>
        /// This field stores the total count of cases that is completed till the DueDate.
        /// </summary>
        public int CompletedCount { get; set; }

        /// <summary>
        /// This field stores accuracy.
        /// </summary>
        public string Accuracy { get; set; }

        /// <summary>
        /// This field stores TAT Breached count.
        /// </summary>
        public int TatBreachedCount { get; set; }
    }
}
