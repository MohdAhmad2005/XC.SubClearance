using System.ComponentModel.DataAnnotations;

namespace XC.XSC.ViewModels.EmailInfoAttachment
{
    public class AddEmailInfoAttachmentRequest
    {
        public long Id { get; set; }
        public long EmailInfoId { get; set; }
        [StringLength(50)]
        public string FileName { get; set; }
        [StringLength(50)]
        public string FileType { get; set; }
        [StringLength(50)]
        public string DocumentId { get; set; }
        [StringLength(50)]
        public string AttachmentId { get; set; }
        public int FileSize { get; set; }
        [StringLength(10)]
        public string SizeUnit { get; set; }
        [StringLength(50)]
        public string TenantId { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
    }
}
