using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.SubmissionStatus
{
    /// <summary>
    /// View model class for Updating existing record.
    /// </summary>
    public class UpdateSubmissionStatusRequest
    {
        /// <summary>
        /// Id of SubmissionStatus (Primary Key) for SubmissionStatus table.
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
        /// This property indicates who is modified the particular SubmissionStatus.
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// This is modified date property and indicates in which date it is modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// This property indicates whether the SubmissionStatus is active or not.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
