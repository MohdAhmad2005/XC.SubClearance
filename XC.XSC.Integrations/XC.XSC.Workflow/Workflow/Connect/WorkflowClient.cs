using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using XC.XSC.Utilities.Request;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.Workflow.Workflow.Models;
using Newtonsoft.Json;
using XC.CCMP.IAM.Keycloak.Models;
using Azure.Core;
using HttpRequest = XC.XSC.Utilities.Request.HttpRequest;

namespace XC.XSC.Workflow.Workflow.Connect
{
    /// <summary>
    /// Implementation of class WorkflowClient.
    /// </summary>
    public class WorkflowClient : IWorkflowClient
    {
        private IWorkflowApiConfig _workflowApiConfig;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpRequest _httpRequest;
        private readonly IResponse _operationResponse;

        public WorkflowClient(IWorkflowApiConfig workflowApiConfig, IHttpContextAccessor httpContextAccessor, IHttpRequest httpRequest, IResponse operationResponse)
        {
            _workflowApiConfig = workflowApiConfig;
            _httpContextAccessor = httpContextAccessor;
            _httpRequest = httpRequest;
            _operationResponse = operationResponse;
        }

        public async Task<IResponse> GetToken(string teamId)
        {
            StringBuilder credentials = new StringBuilder();

            credentials.Append("{");
            credentials.Append($"\"userName\": \"{_workflowApiConfig.AdminUsername}\",");
            credentials.Append($"\"password\": \"{_workflowApiConfig.AdminPassword}\"");
            credentials.Append("}");

            using (HttpClient httpClient = new HttpClient())
            {                
                using (HttpContent httpContent = new StringContent(credentials.ToString()))
                {
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");                   
                    httpContent.Headers.Add("X-TEAM-ID", teamId);
                    
                    var response = await httpClient.PostAsync(_workflowApiConfig.WorkflowAdminLoginEndpoint, httpContent);
                    string content = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var resp = JsonConvert.DeserializeObject<WorkflowLoginSuccessResponse>(content);
                        var operationResponse = new WorkflowTokenResponse
                        {
                            Token = resp.Token,
                            StatusCode = "200",
                            Error = "SUCCESS",
                            ErrorDescription = ""
                        };
                        
                        _operationResponse.IsSuccess = true;
                        _operationResponse.Message = "SUCCESS";
                        _operationResponse.Result = operationResponse;                        
                    }
                    else
                    {
                        var resp = JsonConvert.DeserializeObject<WorkflowLoginFailResponse>(content);
                        var operationResponse = new WorkflowTokenResponse
                        {
                            StatusCode = response.StatusCode.ToString(),
                            Error = response.ReasonPhrase,
                            ErrorDescription = response.ReasonPhrase
                        };
                        
                        _operationResponse.IsSuccess = false;
                        _operationResponse.Message = "Login Failed.";
                        _operationResponse.Result = operationResponse;                        
                    }
                    return _operationResponse;
                }
            }
        }

        /// <summary>
        /// Method to run the camunda workflow.
        /// </summary>
        /// <param name="request">Model class property of type StartWorkflowRequest.</param>
        /// <returns>Camunda workflow process.</returns>
        public async Task<IResponse> RunWorkflow(StartWorkflowRequest request)
        {
            var accessToken = string.Empty;//Generate this token from generate token method.
            var requestData = JsonConvert.SerializeObject(request);

            var response = await _httpRequest.PostAsync(accessToken, _workflowApiConfig.WorkflowEndpoint, requestData);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                _operationResponse.IsSuccess = true;
                _operationResponse.Result = response;
                _operationResponse.Message = "success";
                return _operationResponse;
            }

            _operationResponse.IsSuccess = false;
            _operationResponse.Result = null;
            _operationResponse.Message = "Workflow not started";
            return _operationResponse;
        }
    }
}
