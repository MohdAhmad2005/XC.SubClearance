using XC.XSC.Repositories.MessageSent;
using XC.XSC.Service.User;
using XC.XSC.ViewModels.Configuration;

namespace XC.XSC.Service.MessageSent
{
    public class MessageSentService: IMessageSentService
    {
        private readonly IUserContext _userContext;
        private readonly IMessageSentRepository _messageSentRepository;
        private readonly IResponse _operationResponse;

        public MessageSentService(IMessageSentRepository messageSentRepository, IResponse operationResponse, IUserContext userContext)
        {
            _messageSentRepository = messageSentRepository;
            _operationResponse = operationResponse;
            _userContext = userContext;
        }

        /// <summary>
        /// Add Message sent based on userId
        /// </summary>
        /// <returns></returns>
        public async Task<IResponse> AddMessageSent(Models.Mongo.Entity.MessageSent.MessageSent messageSent)
        {
            var result = await _messageSentRepository.Add(messageSent);
            if (result != null)
            {
                _operationResponse.Result = result;
                _operationResponse.IsSuccess = true;
                _operationResponse.Message = "SUCCESS";
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "No result found";
            }
            return _operationResponse;
        }
    }
}
