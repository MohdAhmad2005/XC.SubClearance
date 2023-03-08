using XC.CCMP.KeyVault;

namespace XC.CCMP.DataStorage
{
    /// <summary>
    /// Configurations of Document Storage API.
    /// </summary>
    public class ApiConfig : IApiConfig
    {
        /// <summary>
        /// Key Vault configuration object.
        /// </summary>
        private readonly IKeyVaultConfig _keyVaultConfig;
        
        /// <summary>
        /// Api Configuration set-up.
        /// </summary>
        /// <param name="keyVaultConfig"></param>
        public ApiConfig (IKeyVaultConfig keyVaultConfig) 
        {            
            _keyVaultConfig = keyVaultConfig;

            this.BaseUrl = _keyVaultConfig.DSApiBaseUrl;
        }

        /// <summary>
        /// Base url of Data Storage Api.
        /// </summary>
        public string BaseUrl { get; set; } = String.Empty;

        /// <summary>
        /// Document upload end point.
        /// </summary>
        public string UploadEndpoint
        {
            get
            {
                return $"{BaseUrl}Document/UploadDocument";
            }
        }

        /// <summary>
        /// Document download endpoint.
        /// </summary>
        public string DownloadEndpoint
        {
            get
            {
                return $"{BaseUrl}Document/DownloadDocument";
            }
        }
    }
}
