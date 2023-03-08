using Newtonsoft.Json;
using System.Text;
using XC.XSC.ValidateMail.Models.Request;
using XC.XSC.ValidateMail.Models.Response;

namespace XC.XSC.ValidateMail.Connect
{
    /// <summary>
    /// Used for connect rule engine api
    /// </summary>
    public class Client : IClient
    {
        private readonly IApiConfig _apiConfig;

        /// <summary>
        /// initialize the class
        /// </summary>
        /// <param name="apiConfig"></param>
        public Client(IApiConfig apiConfig)
        {
            _apiConfig = apiConfig;
        }

        /// <summary>
        /// ExecuteRuleByContextAsync is used to call rule engine Api
        /// </summary>
        /// <param name="ruleExecutionRequest"></param>
        /// <returns>RuleExecutionResult with rule detail and datatable</returns>
        public async Task<RuleExecutionResult> ExecuteRuleByContextAsync(RuleExecutionRequest ruleExecutionRequest)
        {
            return await APIPostRequest<RuleExecutionResult>(_apiConfig.ExecuteRuleByContextEndPoint, JsonConvert.SerializeObject(ruleExecutionRequest));
        }

        /// <summary>
        /// Get rule set list from rule engine
        /// </summary>
        /// <param name="ruleSetRequest"></param>
        /// <returns>ruleSetResponse</returns>
        public async Task<RuleSetResponse> GetRuleSetList(RuleSetRequest ruleSetRequest)
        {
            return await APIPostRequest<RuleSetResponse>(_apiConfig.GetRuleSetListEndPoint, JsonConvert.SerializeObject(ruleSetRequest));            
        }

        /// <summary>
        /// Generic method to post api request
        /// </summary>
        /// <typeparam name="T">Type of the response model.</typeparam>
        /// <param name="apiUrl">API url on which need to post request data.</param>
        /// <param name="requestData">Request data need to post in api request.</param>
        /// <returns></returns>
        private async Task<T> APIPostRequest<T>(string apiUrl, string requestData)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(requestData, Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(apiUrl, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<T>(apiResponse);
                }
            }
        }
    }
}
