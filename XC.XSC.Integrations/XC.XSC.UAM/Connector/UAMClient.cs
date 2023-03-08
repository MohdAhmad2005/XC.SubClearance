using Microsoft.AspNetCore.Http;
using System.Net;
using XC.XSC.UAM.Models;
using Newtonsoft.Json;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.Utilities.Request;
using System.Web;

namespace XC.XSC.UAM.Connector
{
    /// <summary>
    /// Class to invoke api of uam.
    /// </summary>
    public class UAMClient : IUAMClient
    {
        private IUamApiConfig _uamApiConfig;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IResponse _operationResponse;
        private readonly IHttpRequest _httpRequest;

        /// <summary>
        /// Constructor of  uam connector service.
        /// </summary>
        /// <param name="apiConfig"></param>
        /// <param name="httpContextAccessor"></param>
        public UAMClient(IUamApiConfig uamApiConfig, IHttpContextAccessor httpContextAccessor, IResponse operationResponse, IHttpRequest httpRequest)
        {
            _uamApiConfig = uamApiConfig;
            _httpContextAccessor = httpContextAccessor;
            _operationResponse = operationResponse;
            _httpRequest = httpRequest;
        }

        /// <summary>
        /// Method to invoke the list of region from uam.
        /// </summary>
        /// <returns>List of Regions.</returns>
        public async Task<IResponse> GetRegionList()
        {
            var accessToken = string.Empty;
            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null)
                accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
            else
                accessToken = "TestToken";

            var response = await _httpRequest.GetAsync(accessToken, _uamApiConfig.GetRegionListEndPoint);
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                RegionResponse regionResponse = new RegionResponse();
                string apiResponse = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(apiResponse))
                {
                    regionResponse = JsonConvert.DeserializeObject<RegionResponse>(apiResponse);
                    if (regionResponse != null && regionResponse.Result != null)
                    {
                        regionResponse.Result = regionResponse.Result.Select(x => { x.Name = x.RegionName; return x; }).ToList();
                        _operationResponse.IsSuccess = regionResponse.IsSuccess;
                        _operationResponse.Message = regionResponse.Message;
                        _operationResponse.Result = regionResponse.Result;
                        return _operationResponse;
                    }
                }
            }
            _operationResponse.Result = null;
            _operationResponse.IsSuccess = false;
            _operationResponse.Message = "No result found";
            return _operationResponse;
        }

        /// <summary>
        /// Method used to return the Holiday List By TeamId
        /// </summary>
        /// <returns>List of Holiday.</returns>
        public async Task<IResponse> GetHolidayListByTeamId(int teamId)
        {
            var accessToken = string.Empty;
            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null)
                accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
            else
                accessToken = "TestToken";

            var response = await _httpRequest.GetAsync(accessToken, $"{_uamApiConfig.GetHolidayListByTeamIdEndPoint}/{teamId}");
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(apiResponse))
                {
                    var responseResult = JsonConvert.DeserializeObject<HolidayResponse>(apiResponse);
                    if (responseResult != null)
                    {
                        _operationResponse.IsSuccess = responseResult.IsSuccess;
                        _operationResponse.Message = responseResult.Message;
                        _operationResponse.Result = responseResult.Result;
                        return _operationResponse;
                    }
                }
            }
            _operationResponse.Result = null;
            _operationResponse.IsSuccess = false;
            _operationResponse.Message = "No result found";
            return _operationResponse;
        }
         
        /// <summary>
        /// Get team list.
        /// </summary>
        /// <returns>IResponse.</returns>
        public async Task<IResponse> GetTeamList()
        {
            var accessToken = string.Empty;
            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null)
                accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
            else
                accessToken = "TestToken";
            var response = await _httpRequest.GetAsync(accessToken, $"{_uamApiConfig.GetTeamListEndPoint}");

            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var responseResult = JsonConvert.DeserializeObject<TeamResponse>(apiResponse);
                if (responseResult != null && responseResult.Result != null && responseResult.Result.Count > 0)
                {
                    _operationResponse.IsSuccess = responseResult.IsSuccess;
                    _operationResponse.Message = responseResult.Message;
                    _operationResponse.Result = responseResult.Result;
                }
                else
                {
                    _operationResponse.Result = null;
                    _operationResponse.IsSuccess = false;
                    _operationResponse.Message = "No result found";
                }
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "No result found";
            }
            return _operationResponse;
        }

        /// <summary>
        /// Get user's according to given filters.
        /// </summary>
        /// <param name="filterRequest">filter request.</param>
        /// <returns>IResponse.</returns>
        public async Task<IResponse> GetUsersByFilters(UserFilterRequest filterRequest)
        {
            var accessToken = string.Empty;
            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null)
                accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
            else
                accessToken = "TestToken";
            var requestData = JsonConvert.SerializeObject(filterRequest);
            var response = await _httpRequest.PostAsync(accessToken, $"{_uamApiConfig.GetUsersByFiltersEndPoint}", requestData);

            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var responseResult = JsonConvert.DeserializeObject<UserResponse>(apiResponse);
                if (responseResult != null && responseResult.Result != null && responseResult.Result.Count > 0)
                {
                    responseResult.Result = responseResult.Result.Where(item => item.Enabled).Select(x =>
                    { x.Name = string.Format("{0} {1}", x.FirstName, x.LastName); return x; }).ToList();
                    _operationResponse.IsSuccess = responseResult.IsSuccess;
                    _operationResponse.Message = responseResult.Message;
                    _operationResponse.Result = responseResult.Result;
                }
                else
                {
                    _operationResponse.Result = null;
                    _operationResponse.IsSuccess = false;
                    _operationResponse.Message = "No result found";
                }
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "No result found";
            }
            return _operationResponse;
        }

        /// <summary>
        /// Update user details
        /// </summary>
        /// <param name="updateUser"></param>
        /// <returns></returns>
        public async Task<IResponse> UpdateUser(UpdateUser updateUser)
        {
            var accessToken = string.Empty;
            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null)
                accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
            else
                accessToken = "TestToken";
            var requestData = JsonConvert.SerializeObject(updateUser);
            var response = await _httpRequest.PostAsync(accessToken, $"{_uamApiConfig.UpdateUserUAMEndPoint}", requestData);

            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var responseResult = JsonConvert.DeserializeObject<UpdateUserResponse>(apiResponse);
                if (responseResult != null && responseResult.Result != null)
                {
                    _operationResponse.IsSuccess = responseResult.IsSuccess;
                    _operationResponse.Message = responseResult.Message;
                    _operationResponse.Result = responseResult.Result;
                }
                else
                {
                    _operationResponse.Result = null;
                    _operationResponse.IsSuccess = false;
                    _operationResponse.Message = "No result found";
                }
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "No result found";
            }
            return _operationResponse;
        }

        /// <summary>
        /// Get user last login details.
        /// </summary>
        /// <param name="updateUser"></param>
        /// <returns></returns>
        public async Task<IResponse> GetLastLoginDetails()
        {
            var accessToken = string.Empty;
            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null)
                accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
            else
                accessToken = "TestToken";
            var response = await _httpRequest.GetAsync(accessToken, $"{_uamApiConfig.GetUserLastLoginDetailsEndPoint}");

            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var responseResult = JsonConvert.DeserializeObject<UserLastLoginInfoResponse>(apiResponse);
                if (responseResult != null && responseResult.Result != null)
                {
                    _operationResponse.IsSuccess = responseResult.IsSuccess;
                    _operationResponse.Message = responseResult.Message;
                    _operationResponse.Result = responseResult.Result;
                }
                else
                {
                    _operationResponse.Result = null;
                    _operationResponse.IsSuccess = false;
                    _operationResponse.Message = "No result found";
                }
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
