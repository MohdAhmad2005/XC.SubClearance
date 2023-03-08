using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.SubmissionStatus
{
    /// <summary>
    /// It is view model class for Adding new SubmissionStatus.
    /// </summary>
    public class AddSubmissionStatusRequest
    {
        /// <summary>
        /// It is SubmissionStatus name property.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// It is SubmissionStatus color property.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// It is SubmissionStatus lable property and indicates servidity .
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// It is SubmissionStatus order number property.
        /// </summary>
        public int OrderNo { get; set; }

        /// <summary>
        /// It is SubmissionStatus TenantId property.
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// This property indicates who is created the particular SubmissionStatus.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// This property indicates whether the SubmissionStatus is active or not.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
