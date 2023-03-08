namespace XC.XSC.EMS.Model
{
    public class EmailBoxResponse   
    {

        /// <summary>
        /// MailBoxId table PK Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// MailboxEmailID
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// MailBoxId table PK Id
        /// </summary>
        public Guid MailBoxId { get; set; }

        /// <summary>
        /// MailboxEmailID
        /// </summary>
        public string MailboxEmailID { get; set; }

        /// <summary>
        /// LOB_ID
        /// </summary>
        public Guid LOB_ID { get; set; }

        /// <summary>
        /// Interval
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// RegionId
        /// </summary>
        public int RegionId { get; set; }

        /// <summary>
        /// TeamId
        /// </summary>
        public int TeamId { get; set; }
    }

    public class MailBoxList
    {
        /// <summary>
        /// Success status for Region listing.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Message of operation on Region listing.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Result of Region listing.
        /// </summary>
        public List<EmailBoxResponse> Result { get; set; }
    }
}
