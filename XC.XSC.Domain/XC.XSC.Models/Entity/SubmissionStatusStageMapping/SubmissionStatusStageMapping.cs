using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.Models.Interface.SubmissionStatusStageMapping;

namespace XC.XSC.Models.Entity.SubmissionStatusStageMapping
{
    public class SubmissionStatusStageMapping : BaseEntity<int>, ISubmissionStatusStageMapping
    {
        [ForeignKey(nameof(SubmissionStatusId))]
        public int SubmissionStatusId { get; set; }
        [ForeignKey(nameof(SubmissionStageId))]
        public int SubmissionStageId { get; set; }

        public virtual SubmissionStatus.SubmissionStatus SubmissionStatus { get; set; }
        public virtual SubmissionStage.SubmissionStage SubmissionStage { get; set; }

    }
}
