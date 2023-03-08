using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.SubmissionExtraction
{
    /// <summary>
    /// This class used for SubmissionFormData.
    /// </summary>
    public class SubmissionFormData
    {
        /// <summary>
        /// SubmissionData list type property of class SubmissionData.
        /// </summary>
        public List<SubmissionData> SubmissionData { get; set; }
    }

    /// <summary>
    /// This class used for SubmissionData.
    /// </summary>
    public class SubmissionData
    {
        /// <summary>
        /// Fields property for SubmissionData.
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        /// Suggestions property of class Suggestions.
        /// </summary>
        public Suggestions Suggestions { get; set; }

        /// <summary>
        /// GroupName property for SubmissionData.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// FinalEntry property for SubmissionData.
        /// </summary>
        public string FinalEntry { get; set; }

        /// <summary>
        /// Confidance property for SubmissionData.
        /// </summary>
        public string Confidance { get; set; }
    }

    /// <summary>
    /// This class used for holding suggestions.
    /// </summary>
    public class Suggestions
    {
        /// <summary>
        /// Id property for selected suggestion option.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// SuggestionOptions property of type SuggestionOption.
        /// </summary>
        public List<SuggestionOption> SuggestionOptions { get; set; }
    }

    /// <summary>
    /// This class used for suggestion options.
    /// </summary>
    public class SuggestionOption
    {
        /// <summary>
        /// Id property for suggestion option.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name property for suggestion option.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// FinalEntry property for suggestion option.
        /// </summary>
        public string FinalEntry { get; set; }

        /// <summary>
        /// Confidance property for suggestion option.
        /// </summary>
        public string Confidance { get; set; }
    }

}
