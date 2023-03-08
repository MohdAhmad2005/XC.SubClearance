using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.Models.Interface.SubmissionStatusStageMapping
{
    public interface ISubmissionStatusStageMapping
    {
        public int SubmissionStatusId { get; set; }
        public int SubmissionStageId { get; set; }
    }
}
