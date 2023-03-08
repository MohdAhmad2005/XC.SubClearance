using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.CCMP.DataStorage.Models.Request
{
    public class AddDocumentsRequest
    {
        /// <summary>
        /// String Tenant Id
        /// </summary>
        [Required(ErrorMessage = "TenantId is required")]
        public string TenantId { get; set; }

        [Required(ErrorMessage = "UploadedBy is required")]
        public string UploadedBy { get; set; }

        [Required(ErrorMessage = "FileGroup is required")]
        public AddFiles FileGroup { get; set; }

    }
}
