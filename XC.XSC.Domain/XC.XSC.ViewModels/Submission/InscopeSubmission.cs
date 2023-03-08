using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.Submission
{
    public class InScopeSubmission
    {
        public InScopeSubmission()
        {
            this.Submissions = new List<SubmissionResponse>();
        }
        public int CurrentPage { get; init; }

        public int TotalItems { get; init; }

        public int TotalPages { get; init; }

        public List<SubmissionResponse> Submissions { get; init; }
    }
}
