using XC.XSC.Repositories.Comment;
using XC.XSC.Service.User;
using XC.XSC.ViewModels;
using XC.XSC.ViewModels.Comment;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.Enum;

namespace XC.XSC.Service.Comments
{
    public class CommentService : ICommentService
    {
        private readonly IUserContext _userContext;
        private readonly ICommentRepository _commentRepository;
        private readonly IResponse _operationResponse;

        /// <summary>
        /// Comment Service constructor
        /// </summary>
        /// <param name="userContext">Logged-In user context.</param>
        /// <param name="commentRepository">Comment repository.</param>
        /// <param name="operationResponse">Returns generic IResponse.</param>
        public CommentService(IUserContext userContext, ICommentRepository commentRepository, IResponse operationResponse)
        {
            _userContext = userContext;
            _operationResponse = operationResponse;
            _commentRepository = commentRepository;
        }

        /// <summary>
        /// Add Comments
        /// </summary>
        /// <param name="comment"> Comment request.</param>
        /// <returns>Returns bool: Success then true and false if failed.</returns>
        public async Task<IResponse> AddComment(CommentRequest commentRequest)
        {
            Models.Entity.Comment.Comment comments = new Models.Entity.Comment.Comment()
            {
                CommentText = commentRequest.CommentText,
                CommentTypeId = (int)(CommentType)System.Enum.Parse(typeof(CommentType), commentRequest.CommentType),
                TenantId = _userContext.UserInfo.TenantId,
                CreatedBy = _userContext.UserInfo.UserId,
                CreatedDate = DateTime.Now,
                SubmissionId = commentRequest.SubmissionId,
                JsonData = commentRequest.JsonData,
                IsActive = true,
            };

            await _commentRepository.AddAsync(comments);
            
            _operationResponse.IsSuccess = true;
            _operationResponse.Message = "SUCCESS";
            
            return _operationResponse;

        }
    }
}
