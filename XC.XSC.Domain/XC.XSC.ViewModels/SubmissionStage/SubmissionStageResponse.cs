using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.SubmissionStage
{
    /// <summary>
    /// This is a response model. It helps to show the properties in UI.
    /// It contains properties like-Name,color,Label,OrderNo,TenantId and CreatedBy.
    /// </summary>
    public class SubmissionStageResponse
    {
        /// <summary>
        /// It is SubmissionStage name property.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// It is SubmissionStage color property.
        /// </summary>
        public string? Color { get; set; }

        /// <summary>
        /// It is SubmissionStage lable property and indicates servidity .
        /// </summary>
        public string? Label { get; set; }

        /// <summary>
        /// It is SubmissionStage order number property.
        /// </summary>
        public int OrderNo { get; set; }

        /// <summary>
        /// It is SubmissionStage TenantId property.
        /// </summary>
        public string? TenantId { get; set; }

        /// <summary>
        /// This property indicates who is submitted the particular SubmissionStage.
        /// </summary>
        public string? CreatedBy { get; set; }
    }
}
