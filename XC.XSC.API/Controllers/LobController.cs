using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XC.XSC.Service.Submission;
using XC.XSC.Service.SubmissionClearance;
using XC.XSC.Service.User;
using XC.XSC.Service.ValidateMail;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels;
using XC.XSC.Service.Lobs;
using XC.XSC.ViewModels.Submission;
using XC.XSC.Models.Entity.Lob;

namespace XC.XSC.API.Controllers
{
    /// <summary>
    /// Controller of Lob.
    /// </summary>
    [Authorize]
    [Route("api/xsc/[controller]")]
    [ApiController]
    public class LobController : Controller
    {
        private readonly IUserContext _userContext;
        private readonly ILobService _lob;
        private readonly IResponse _operationResponse;

        /// <summary>
        /// This is constructor of Lob controller.
        /// </summary>
        /// <param name="userContext"></param>
        /// <param name="operationResponse"></param>
        /// <param name="lob"></param>
        public LobController(IUserContext userContext, IResponse operationResponse, ILobService lob)
        {
            _userContext = userContext;
            _operationResponse = operationResponse;
            _lob = lob;
        }

        /// <summary>
        /// Get all list of lob.
        /// </summary>
        /// <returns>List of lob.</returns>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IResponse> GetAll()
        {
            var lobList = await _lob.GetLobsAsync(_userContext.UserInfo.TenantId);
            if (lobList.Any())
            {
                _operationResponse.IsSuccess = true;
                _operationResponse.Message = "SUCCESS";
                _operationResponse.Result = lobList;
            }
            else
            {

                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "No records found.";
                _operationResponse.Result = null;
            }
            return _operationResponse;
        }

        /// <summary>
        /// Get all list of lob of a loged in user.
        /// </summary>
        /// <returns>List of lobs of a logged in user.</returns>
        [HttpGet]
        [Route("getUserLob")]
        public async Task<IResponse> GetUserLob()
        {
            var lobList = await _lob.GetUserLob();
            if (lobList.Any())
            {
                _operationResponse.IsSuccess = true;
                _operationResponse.Message = "SUCCESS";
                _operationResponse.Result = lobList;
            }
            else
            {

                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "No records found.";
                _operationResponse.Result = null;
            }
            return _operationResponse;
        }
    }
}
