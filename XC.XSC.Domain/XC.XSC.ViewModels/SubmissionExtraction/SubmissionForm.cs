using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.SubmissionExtraction
{
    /// <summary>
    /// SubmissionEditForm class for Submission form response.
    /// </summary>
    public class SubmissionEditForm
    {
        /// <summary>
        /// SubmissionForm property of type SubmissionForm.
        /// </summary>
        public List<SubmissionForm> SubmissionForm { get; set; }
    }

    /// <summary>
    /// SubmissionForm class.
    /// </summary>
    public class SubmissionForm
    {
        /// <summary>
        /// Fields property of type SubmissionForm.
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        /// GroupName property of type SubmissionForm.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Suggestions property of type SubmissionForm.
        /// </summary>
        public Suggestions Suggestions { get; set; }

        /// <summary>
        /// FinalEntry property of type SubmissionForm.
        /// </summary>
        public string FinalEntry { get; set; }

        /// <summary>
        /// Confidance property of type SubmissionForm.
        /// </summary>
        public string Confidance { get; set; }

        /// <summary>
        /// Controls property of type SubmissionForm.
        /// </summary>
        public List<Controls> Controls { get; set; }

        /// <summary>
        /// Visible property of type SubmissionForm.
        /// </summary>
        public bool Visible { get; set; }
    }

    /// <summary>
    /// Controls Class.
    /// </summary>
    public class Controls
    {
        /// <summary>
        /// Key property of type Controls.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Control property of type Controls.
        /// </summary>
        public string Control { get; set; } = String.Empty;

        /// <summary>
        /// Required property of type Controls.
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// RequiredMessage property of type Controls.
        /// </summary>
        public string RequiredMessage { get; set; } = String.Empty;
    }

}
