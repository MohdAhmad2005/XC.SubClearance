namespace XC.XSC.Utilities.Request
{
    public interface IHttpRequest
    {
        /// <summary>
        /// Generic method to post api request
        /// </summary>
        /// <param name="apiUrl">API url on which need to post request data.</param>
        /// <param name="accessToken">Access token which need to add in request header.</param>
        /// <param name="requestData">Request data need to post in api request.</param>
        /// <returns></returns>
        Task<HttpResponseMessage> PostAsync(string accessToken, string apiUrl, string requestData);


        /// <summary>
        /// Generic method to Get api request
        /// </summary>
        /// <param name="accessToken">Access token which need to add in request header.</param>
        /// <param name="apiUrl">API url on which need to post request data.</param>
        /// <returns></returns>
        Task<HttpResponseMessage> GetAsync(string accessToken, string apiUrl);
       
    }
}
