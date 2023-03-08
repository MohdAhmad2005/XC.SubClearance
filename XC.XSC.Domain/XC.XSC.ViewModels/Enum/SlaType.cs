using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace XC.XSC.ViewModels.Enum
{
    public enum SlaType
    {
        /// <summary>
        /// Sla type is TAT.
        /// </summary>
        [Display(Name = "TAT")]
        TAT = 1,

        /// <summary>
        ///Sla Type Accuracy.
        /// </summary>
        [Display(Name = "Accuracy")]
        Accuracy = 2,
    }
}
