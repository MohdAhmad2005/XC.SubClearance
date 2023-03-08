using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using XC.XSC.EMS.Model;
using XC.XSC.Utilities.Request;
using XC.XSC.ViewModels.Configuration;

namespace XC.XSC.EMS.Connector
{
    public class EMSClient: IEMSClient
    {
        private IEmsApiConfig _emsApiConfig;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IResponse _operationResponse;
        private readonly IHttpRequest _httpRequest;

        /// <summary>
        /// Constructor of  ems connector service.
        /// </summary>
        /// <param name="apiConfig"></param>
        /// <param name="httpContextAccessor"></param>
        public EMSClient(IEmsApiConfig emsApiConfig, IHttpContextAccessor httpContextAccessor, IResponse operationResponse, IHttpRequest httpRequest)
        {
            _emsApiConfig = emsApiConfig;
            _httpContextAccessor = httpContextAccessor;
            _operationResponse = operationResponse;
            _httpRequest = httpRequest;
        }

        /// <summary>
        /// Method to invoke the list of region from uam.
        /// </summary>
        /// <returns>List of Mail Box.</returns>
        public async Task<IResponse> GetMailBoxList(int regionId, string lobId, int teamId, string tenantId)
        {
            var accessToken = string.Empty;
            if (_httpContextAccessor != null && _httpContextAccessor.HttpContext != null)
            {
                if (!string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString()))
                {
                    accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];
                }
            }
            else
                accessToken = "TestToken";

            var response = await _httpRequest.GetAsync(accessToken, $"{_emsApiConfig.EmailBoxListEndPoint}/{regionId}/{lobId}/{teamId}/{tenantId}");
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                MailBoxList emailBoxResponse = new MailBoxList();
                string apiResponse = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(apiResponse))
                {
                    emailBoxResponse = JsonConvert.DeserializeObject<MailBoxList>(apiResponse);
                    if (emailBoxResponse != null && emailBoxResponse.Result != null)
                    {
                        emailBoxResponse.Result = emailBoxResponse.Result.Select(x => { x.Name = x.MailboxEmailID; x.Id = x.MailBoxId; return x; }).ToList();
                        _operationResponse.IsSuccess = emailBoxResponse.IsSuccess;
                        _operationResponse.Message = emailBoxResponse.Message;
                        _operationResponse.Result = emailBoxResponse.Result;
                        return _operationResponse;
                    }
                }
                else
                {
                    _operationResponse.Result = null;
                    _operationResponse.IsSuccess = false;
                    _operationResponse.Message = $"No result found";
                }
            }
            _operationResponse.Result = null;
            _operationResponse.IsSuccess = false;
            _operationResponse.Message = $"An error has occured: {response.ReasonPhrase}";

            return _operationResponse;
        }
    }
}
