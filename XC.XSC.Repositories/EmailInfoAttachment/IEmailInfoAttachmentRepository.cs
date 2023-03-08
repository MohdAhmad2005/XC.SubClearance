using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.Repositories.EmailInfoAttachment
{
    public interface IEmailInfoAttachmentRepository : IRepository<Models.Entity.EMailInfoAttachment.EmailInfoAttachment>
    {
        Task AddListAsync(List<Models.Entity.EMailInfoAttachment.EmailInfoAttachment> addEmailInfoAttachmentRequest);
    }
}
