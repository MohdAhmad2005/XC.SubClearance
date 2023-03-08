namespace XC.CCMP.DataStorage
{
    public interface IApiConfig
    {
        /// <summary>
        /// Base url of Data Storage Api.
        /// </summary>
        public string BaseUrl { get; set; }
        
        /// <summary>
        /// Document upload end point.
        /// </summary>
        public string UploadEndpoint { get; }
        
        /// <summary>
        /// Document download endpoint.
        /// </summary>
        public string DownloadEndpoint { get; }
    }
}
