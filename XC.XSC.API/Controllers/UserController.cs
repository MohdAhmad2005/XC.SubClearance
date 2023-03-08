using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XC.XSC.Service.Submission;
using XC.XSC.Service.SubmissionClearance;
using XC.XSC.Service.User;
using XC.XSC.Service.ValidateMail;
using XC.XSC.UAM.Models;
using XC.XSC.UAM.UAM;
using XC.XSC.ViewModels.Configuration;
using Attribute = XC.XSC.UAM.Models.Attribute;

namespace XC.XSC.API.Controllers
{
    /// <summary>
    /// This is the Controller class containing the methods that interacts with UAM.
    /// </summary>
    [Authorize]
    [Route("api/xsc/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserContext _userContext;
        private readonly IUamService _uamService;
        private readonly IResponse _operationResponse;

        /// <summary>
        /// user construtor.
        /// </summary>
        /// <param name="userContext"> Login user detail. </param>
        /// <param name="uamService"> uam Service.</param>
        /// <param name="operationResponse">Response of IResponse type.</param>
        public UserController(IUserContext userContext, IResponse operationResponse, IUamService uamService)
        {
            _userContext = userContext;
            _operationResponse = operationResponse;
            _uamService = uamService;
        }

        /// <summary>
        /// get region list.
        /// </summary>
        /// <returns>return the region list response as common IResponse.</returns>
        [HttpGet]
        [Route("getUserRegions")]
        public async Task<IResponse> GetUserRegions()
        {
            var userRegionList = await _uamService.GetUserRegions();
            if (userRegionList.Result != null && ((List<Region>)userRegionList.Result).Count > 0)
            {
                _operationResponse.IsSuccess = userRegionList.IsSuccess;
                _operationResponse.Message = userRegionList.Message;
                _operationResponse.Result = userRegionList.Result;
            }
            else
            {

                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "NO REGIONS FOUND";
                _operationResponse.Result = null;
            }
            return _operationResponse;
        }

        /// <summary>
        /// Get list of users according to given filters.
        /// </summary>
        /// <param name="filterRequest">Model class property of type UserFilterRequest.</param>
        /// <returns>Returns User details with respect to filters as IResponse.</returns>
        [HttpPost]
        [Route("getUsersByFilters")]
        [Authorize]
        public async Task<IResponse> GetUsersByFilters(UserFilterRequest filterRequest)
        {
            return await _uamService.GetUsersByFilters(filterRequest);
        }

        /// <summary>
        /// Get current user details.
        /// </summary>
        /// <returns>Current user details from keycloak.</returns>
        [HttpGet]
        [Route("getCurrentUserDetails")]
        [Authorize]
        public async Task<IResponse> GetCurrentUserDetails()
        {
            UserFilterRequest userFilterRequest = new UserFilterRequest();
            userFilterRequest.Attributes.Add(new Attribute() { Name = "UserId", Value = new List<string>() { _userContext.UserInfo.UserId } });
            return await GetUsersByFilters(userFilterRequest);
        }

        /// <summary>
        /// Update user details.
        /// </summary>
        /// <param name="updateUser"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updateUser")]
        [Authorize]
        public async Task<IResponse> UpdateUser(UpdateUser updateUser)
        {
            return await _uamService.UpdateUser(updateUser);
        }

        /// <summary>
        /// Method to return user last login details from UAM.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getLastLoginDetails")]
        [Authorize]
        public async Task<IResponse> GetLastLoginDetails()
        {
            return await _uamService.GetLastLoginDetails();
        }
    }
}
