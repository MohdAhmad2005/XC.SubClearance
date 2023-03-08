using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.Submission
{
    /// <summary>
    /// This class is written to get the count of Submissions.
    /// </summary>
    public class SubmissionScopeCountResponse
    {
        /// <summary>
        ///  This is the total count of submissions that filtered in provided date range
        /// </summary>
        public int TotalCount { get; set;}

        /// <summary>
        ///  This is the Inscope count of submissions that filtered in provided date range.
        /// </summary>
        public int InScopeCount { get; set;}

        /// <summary>
        ///  This is the Outscope count of submissions that filtered in provided date range.
        /// </summary>
        public int OutScopeCount { get; set;}
    }
}
