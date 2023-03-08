using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.SubmissionStage
{
    /// <summary>
    /// It is a request model and it intracts with user while user wants to update a record in database.
    /// It contains properties like-Id, Name, color, Label, OrderNo, TenantId, ModifiedBy, ModifiedDate and IsActive.
    /// </summary>
    public class UpdateSubmissionStageRequest
    {
        /// <summary>
        /// Id of SubmissionStatus (Primary Key) for SubmissionStage table.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// It is SubmissionStage name property.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// It is SubmissionStage color property.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// It is SubmissionStage lable property and indicates servidity .
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// It is SubmissionStage order number property.
        /// </summary>
        public int OrderNo { get; set; }

        /// <summary>
        /// It is SubmissionStage TenantId property.
        /// </summary>
        public string TenantId { get; set; }
        /// <summary>
        /// This property indicates who is modified the particular SubmissionStage.
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// This is modified date property and indicates in which date it is modified.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// This property indicates whether the SubmissionStage is active or not.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
