using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.Data;
using XC.XSC.ViewModels.EmailInfo;

namespace XC.XSC.Repositories.Comment
{
    public class CommentRepository : ICommentRepository
    {
        private readonly MSSqlContext _msSqlContext;
        public CommentRepository(MSSqlContext msSqlContext)
        {
            _msSqlContext = msSqlContext;
        }
        /// <summary>
        /// Add Comment according comments type
        /// </summary>
        /// <param name="addCommentRequest"></param>
        /// <returns></returns>
        public async Task AddAsync(Models.Entity.Comment.Comment addCommentRequest)
        {
            _msSqlContext.Comments.Add(addCommentRequest);
            await _msSqlContext.SaveChangesAsync();
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task DeleteAsync(Expression<Func<Models.Entity.Comment.Comment, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is implemented to retrieve all record of Comments table.
        /// </summary>
        /// <returns> Returns all data from Comments table</returns>
        public IQueryable<Models.Entity.Comment.Comment> GetAll()
        {
            return _msSqlContext.Comments.AsQueryable();
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Models.Entity.Comment.Comment> GetSingleAsync(Expression<Func<Models.Entity.Comment.Comment, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="obj">Comment object</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Models.Entity.Comment.Comment> UpdateAsync(Models.Entity.Comment.Comment obj)
        {
            throw new NotImplementedException();
        }
    }
}
