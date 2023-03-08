using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XC.XSC.Models.Interface.EmailInfoAttachment;

namespace XC.XSC.Models.Entity.EMailInfoAttachment
{
    public class EmailInfoAttachment : BaseEntity<long>, IEmailInfoAttachment
    {

        [Required]
        [ForeignKey(nameof(EmailInfoId))]
        public long EmailInfoId { get; set ; }

        [Required]
        [Range(1,500,ErrorMessage ="Max allowed file name length is 500.")]        
        [StringLength(500)]
        public string FileName { get; set; }

        [Range(1, 50, ErrorMessage = "Max allowed FileType length is 50.")]
        [StringLength(50)]
        public string FileType { get; set; }

        [Range(1, 50, ErrorMessage = "Max allowed document Id length is 50.")]
        [StringLength(50)]
        public string DocumentId { get; set; }

        [Range(1, 50, ErrorMessage = "Max allowed AttachmentId length is 50.")]
        [StringLength(50)]
        public string AttachmentId { get; set; }
        
        public int FileSize { get; set; }

        [Range(1, 5, ErrorMessage = "Max allowed SizeUnit length is 5.")]
        [StringLength(5)]
        public string SizeUnit { get; set; }

        public virtual EmailInfo.EmailInfo EmailInfo { get; set; }

    }
}
