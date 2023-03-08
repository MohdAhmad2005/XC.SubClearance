using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.Authentication
{
    public class ForgotPasswordRequest
    {
        [Required(ErrorMessage = "UserId is required")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "NewPassword is required")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is required")]
        [Compare("NewPassword", ErrorMessage = "The '{1}' and '{0}' fields do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
