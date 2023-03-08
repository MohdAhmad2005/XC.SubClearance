using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.TenantActionDetail
{
    public class TenantActionRequest
    {
        /// <summary>
        /// This property used for TenantId
        /// </summary>
        [Required(ErrorMessage ="TenantId is Required")]
        public string TenantId { get; set; }

        /// <summary>
        /// This property used for Submission Status id
        /// </summary>

        [Range(1, int.MaxValue, ErrorMessage = "Status Id is Required")]

        public int StatusId { get; set; }

        /// <summary>
        /// This property used  User role name it optional
        /// </summary>
        public string? RoleName { get; set; }
    }
}
