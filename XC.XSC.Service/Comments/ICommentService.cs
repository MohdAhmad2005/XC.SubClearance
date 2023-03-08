using XC.XSC.ViewModels.Comment;
using XC.XSC.ViewModels.Configuration;

namespace XC.XSC.Service.Comments
{
    public interface ICommentService
    {
        /// <summary>
        /// Add Comments
        /// </summary>
        /// <param name="comment"> Comment request.</param>
        /// <returns>Returns bool: Success then true and false if failed.</returns>
        Task<IResponse> AddComment(CommentRequest comment);
    }
}
