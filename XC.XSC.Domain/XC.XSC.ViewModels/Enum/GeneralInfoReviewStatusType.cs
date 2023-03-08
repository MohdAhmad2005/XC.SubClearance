using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.Enum
{
    public enum GeneralInfoReviewStatusType
    {
        /// <summary>
        /// Review Status Pass.
        /// </summary>
        [Display(Name = "Pass")]
        Pass = 10,

        /// <summary>
        /// Review Status Fail.
        /// </summary>
        [Display(Name = "Fail")]
        Fail = 9
    }
}
