using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using XC.XSC.Data;

namespace XC.XSC.Repositories.EmailInfo
{
    public class EmailInfoRepository : IEmailInfoRepository
    {
        private readonly MSSqlContext _msSqlContext;
        public EmailInfoRepository(MSSqlContext msSqlContext)
        {
            _msSqlContext = msSqlContext;
        }
        /// <summary>
        /// Add Email Infos
        /// </summary>
        /// <param name="addEmailInfoRequest"></param>
        /// <returns></returns>
        public async Task AddAsync(Models.Entity.EmailInfo.EmailInfo addEmailInfoRequest)
        {
            _msSqlContext.EmailInfos.Add(addEmailInfoRequest);
            await _msSqlContext.SaveChangesAsync();
        }

        public Task DeleteAsync(Expression<Func<Models.Entity.EmailInfo.EmailInfo, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Models.Entity.EmailInfo.EmailInfo> GetAll()
        {
            return _msSqlContext.EmailInfos.Include(attachments => attachments.Attachments)
                                                  .AsQueryable();
        }

        public async Task<Models.Entity.EmailInfo.EmailInfo> GetSingleAsync(Expression<Func<Models.Entity.EmailInfo.EmailInfo, bool>> predicate)
        {
            return await _msSqlContext.EmailInfos.Include(attachments => attachments.Attachments)
                                      .SingleOrDefaultAsync(predicate);
        }

        public Task<Models.Entity.EmailInfo.EmailInfo> UpdateAsync(Models.Entity.EmailInfo.EmailInfo obj)
        {
            throw new NotImplementedException();
        }
    }
}
