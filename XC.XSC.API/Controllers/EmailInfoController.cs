using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XC.XSC.Service.EmailInfo;
using XC.XSC.Service.EMS;
using XC.XSC.ViewModels;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.EmailInfo;

namespace XC.XSC.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/xsc/[controller]")]
    public class EmailInfoController : ControllerBase
    {
        private readonly IEmailInfoService _EmailInfoService;
        private readonly IResponse _operationResponse;
        private readonly IEmsService _emsService;

        /// <summary>
        /// Email info constructor.
        /// </summary>
        /// <param name="emailInfoService">email info service instance.</param>
        /// <param name="operationResponse"> operation response instance.</param>
        public EmailInfoController(IEmailInfoService emailInfoService, IResponse operationResponse, IEmsService emsService)
        {
            _EmailInfoService = emailInfoService;
            _operationResponse = operationResponse;
            _emsService = emsService;
        }

        /// <summary>
        /// This method is responsible for add emailInfo to the database.
        /// </summary>
        /// <param name="emailInfoRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("saveEmailInfo")]
        [Authorize]
        public async Task<IResponse> SaveEmailInfoWithAttachment(AddEmailInfoRequest emailInfoRequest)
        {
            return await _EmailInfoService.SaveEmailInfo(emailInfoRequest);
        }

        ///<summary>
        ///Get email info details based on the email info id.
        /// </summary>
        /// <param name="emailInfoId">email info id.</param>
        /// <returns>return the single email info response as common IResponse.</returns>
        [HttpGet]
        [Route("getEmailInfoDetailById/{emailInfoId}")]
        [Authorize]
        public async Task<IResponse> GetEmailInfoDetailById(long emailInfoId)
        {
            if (emailInfoId > 0)
            {
                return await _EmailInfoService.GetEmailInfoDetailByIdAsync(emailInfoId);
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "In-Valid email info id.";
                return _operationResponse;
            }
        }

        /// <summary>
        /// Get mail box details based on the following parameter.
        /// </summary>
        /// <param name="regionId">region id.</param>
        /// <param name="lobId">lob id.</param>
        /// <param name="teamId">team id.</param>
        /// <returns>IResponse.</returns>
        [HttpGet]
        [Route("getMailBoxList")]
        public async Task<IResponse> getMailBoxList(int regionId, int lobId, int teamId)
        {
            return await _emsService.GetMailBoxList(regionId, lobId, teamId);
        }
    }
}
