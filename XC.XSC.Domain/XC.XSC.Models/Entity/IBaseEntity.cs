namespace XC.XSC.Models.Entity
{
    public interface IBaseEntity<T>
    {
        public T Id { get; set; }
        public string TenantId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public bool IsActive { get; set; }        
    }
}
