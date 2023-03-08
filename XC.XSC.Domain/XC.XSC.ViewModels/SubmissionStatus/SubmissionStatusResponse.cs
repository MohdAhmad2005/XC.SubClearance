using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.SubmissionStatus
{
    /// <summary>
    /// Response view model for get-all and get-single Api.
    /// </summary>
    public class SubmissionStatusResponse
    {
        /// <summary>
        /// Id of the corresponding submission status
        /// </summary>
        public int Id { get; set; }

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
        /// This is Created date property and indicates in which date it is created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// This property indicates who is created the particular SubmissionStatus.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// how many status is present in the table
        /// </summary>
        public int StatusCount { get; set; }

        /// <summary>
        /// Check whether the submission status is active or not
        /// </summary>
        public bool IsActive { get; set; }

    }
}
