namespace XC.XSC.Models.Mongo
{
    public interface IBaseEntity
    {
        /// <summary>
        /// Gets or Sets TenantId
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// Gets or Sets CreatedDate
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or Sets CreatedBy
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or Sets ModifiedDate
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or Sets ModifiedBy
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Gets or Sets IsActive
        /// </summary>
        public bool IsActive { get; set; }
    }
}
