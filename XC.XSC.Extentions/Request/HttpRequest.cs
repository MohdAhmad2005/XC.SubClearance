using System.Net.Http.Headers;
using System.Text;

namespace XC.XSC.Utilities.Request
{
    public class HttpRequest: IHttpRequest
    {
        /// <summary>
        /// Generic method to post api request
        /// </summary>
        /// <param name="apiUrl">API url on which need to post request data.</param>
        /// <param name="accessToken">Access token which need to add in request header.</param>
        /// <param name="requestData">Request data need to post in api request.</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostAsync(string accessToken, string apiUrl, string requestData)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(requestData, Encoding.UTF8, "application/json");
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    }
                    return await httpClient.PostAsync(apiUrl, content);
                }
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage();
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
            
        }


        /// <summary>
        /// Generic method to Get api request
        /// </summary>
        /// <param name="accessToken">Access token which need to add in request header.</param>
        /// <param name="apiUrl">API url on which need to post request data.</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetAsync(string accessToken, string apiUrl)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    }
                    return await httpClient.GetAsync(apiUrl);
                }
            }
            catch(Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage();
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.ReasonPhrase = ex.Message;

                return response;
            }
            
        }
    }
}
