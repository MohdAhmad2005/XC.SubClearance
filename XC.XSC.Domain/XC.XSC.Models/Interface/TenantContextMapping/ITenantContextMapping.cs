namespace XC.XSC.Models.Interface.TenantContextMapping
{
    /// <summary>
    /// This interface is used to return the TenantContextMappings table data
    /// </summary>
    public interface ITenantContextMapping
    {

        /// <summary>
        /// Region of the corresponding TenantContextMapping
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Lob of the corresponding TenantContextMapping
        /// </summary>
        public string Lob { get; set; }

        /// <summary>
        /// ContextId of the corresponding TenantContextMapping
        /// </summary>
        public int ContextId { get; set; }

        /// <summary>
        /// EntityId of the corresponding TenantContextMapping
        /// </summary>
        public int EntityId { get; set; }
    }
}
