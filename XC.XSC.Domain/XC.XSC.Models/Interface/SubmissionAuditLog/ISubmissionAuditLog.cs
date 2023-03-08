namespace XC.XSC.Models.Interface.SubmissionAuditLog
{
    public interface ISubmissionAuditLog
    {
        /// <summary>
        /// This field is used to store the SubmissionId.
        /// </summary>
        public long SubmissionId { get; set; }

        /// <summary>
        /// This field is used to store the PrevStatus.
        /// </summary>
        public int PrevStatus { get; set; }

        /// <summary>
        /// This field is used to store the NewStatus.
        /// </summary>
        public int NewStatus { get; set; }

        /// <summary>
        /// This field is used to store the PrevAssignedToId.
        /// </summary>

        public string PrevAssignedToId { get; set; }

        /// <summary>
        /// This field is used to store the NewAssignedToId.
        /// </summary>
        public string NewAssignedToId { get; set; }


    }
}
