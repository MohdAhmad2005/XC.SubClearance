namespace XC.XSC.ValidateMail.Models.Response
{
    public class ExecutedRule
    {
        /// <summary>
        /// RuleName to execute
        /// </summary>
        public string? RuleName { get; set; }
        /// <summary>
        /// Rule Error Message
        /// </summary>
        public string? ErrorMessage { get; set; }
        /// <summary>
        /// RowNumbers
        /// </summary>
        public string? RowNumbers { get; set; }
    }
}
