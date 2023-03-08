using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.Data;
using XC.XSC.ViewModels.EmailInfo;

namespace XC.XSC.Repositories.EmailInfoAttachment
{
    public class EmailInfoAttachmentRepository : IEmailInfoAttachmentRepository
    {
        private readonly MSSqlContext _msSqlContext;
        public EmailInfoAttachmentRepository(MSSqlContext msSqlContext)
        {
            _msSqlContext = msSqlContext;
        }

        public Task AddAsync(Models.Entity.EMailInfoAttachment.EmailInfoAttachment obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add EmailInfo Attachments 
        /// </summary>
        /// <param name="addEmailInfoAttachmentRequest"></param>
        /// <returns></returns>
        public async Task AddListAsync(List<Models.Entity.EMailInfoAttachment.EmailInfoAttachment> addEmailInfoAttachmentRequest)
        {
            _msSqlContext.EmailInfoAttachments.AddRangeAsync(addEmailInfoAttachmentRequest);
            await _msSqlContext.SaveChangesAsync();
        }

        public Task DeleteAsync(Expression<Func<Models.Entity.EMailInfoAttachment.EmailInfoAttachment, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Models.Entity.EMailInfoAttachment.EmailInfoAttachment> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Models.Entity.EMailInfoAttachment.EmailInfoAttachment> GetSingleAsync(Expression<Func<Models.Entity.EMailInfoAttachment.EmailInfoAttachment, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Entity.EMailInfoAttachment.EmailInfoAttachment> UpdateAsync(Models.Entity.EMailInfoAttachment.EmailInfoAttachment obj)
        {
            throw new NotImplementedException();
        }
    }
}
