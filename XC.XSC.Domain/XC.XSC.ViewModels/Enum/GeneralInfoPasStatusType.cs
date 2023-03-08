using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.Enum
{
    public enum GeneralInfoPasStatusType
    {
        /// <summary>
        /// PAS Status Pass.
        /// </summary>
        [Display(Name = "Pass")]
        Pass = 1,

        /// <summary>
        /// PAS Status Fail.
        /// </summary>
        [Display(Name = "Fail")]
        Fail = 0
    }
}
