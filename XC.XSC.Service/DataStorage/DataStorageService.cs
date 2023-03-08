using XC.CCMP.DataStorage;
using XC.CCMP.DataStorage.Connect;
using XC.CCMP.DataStorage.Models.Response;

namespace XC.XSC.Service.DataStorage
{
    /// <summary>
    /// This class will handle all the document related services.
    /// </summary>
    public class DataStorageService : IDataStorageService
    {
        private readonly IClient _client;

        /// <summary>
        /// This is the data storage service class constructor.
        ///<param name="client">datastorage connect library instance.</param>
        /// </summary>
        public DataStorageService(IClient client)
        {
            _client = client;
        }

        /// <summary>
        /// This method is used to download document into storage location using Data Storage API.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="documentId"></param>
        /// <returns>DownloadDocumentResponse model.</returns>
        public async Task<DownloadDocumentResponse> DownloadDocumentAsync(string tenantId, string documentId)
        {
            var result =await _client.DownloadDocumentAsync(tenantId, documentId);
            if (result != null)
            {
                return result;
            }
            else
            {
                return new DownloadDocumentResponse();
            }
        }
    }
}
