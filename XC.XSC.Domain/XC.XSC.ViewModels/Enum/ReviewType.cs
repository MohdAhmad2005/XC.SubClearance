using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace XC.XSC.ViewModels.Enum
{
    public enum ReviewType
    {
        /// <summary>
        /// Skip review type.
        /// </summary>
        [Display(Name = "Skip Review")]
        SkipReview = 1,

        /// <summary>
        /// Compulsary review type.
        /// </summary>
        [Display(Name = "Compulsary Review")]
        CompulsaryReview = 2,

        /// <summary>
        /// Percentage review.
        /// </summary>
        [Display(Name ="% age Review")]
        PercentageReview=3
    }
}
