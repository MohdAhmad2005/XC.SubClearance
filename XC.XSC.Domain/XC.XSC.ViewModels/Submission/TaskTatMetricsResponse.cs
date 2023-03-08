namespace XC.XSC.ViewModels.Submission
{
    public class TaskTatMetricsResponse
    {
        /// <summary>
        /// Gets or sets the ReceivedDate
        /// </summary>
        public DateTime ReceivedDate { get; set; }

        /// <summary>
        /// Gets or sets the DefinedTat
        /// </summary>
        public int DefinedTat { get; set; }

        /// <summary>
        /// Gets or sets the DueDate
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Gets or sets the TatMissed
        /// </summary>
        public bool TatMissed { get; set; }

        /// <summary>
        /// Gets or sets the DaysOverdue
        /// </summary>
        public int DaysOverdue { get; set; } = 0;

        /// <summary>
        /// Gets or sets the BusinessDaysText
        /// </summary>
        public string BusinessDaysText { get; set; }
    }
}
