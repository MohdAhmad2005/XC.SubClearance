using XC.CCMP.DataStorage.Models.Request;
using XC.CCMP.DataStorage.Models.Response;

namespace XC.CCMP.DataStorage.Connect
{
    public interface IClient
    {
        /// <summary>
        /// This method is used to upload document into storage location using Data Storage API.
        /// </summary>
        /// <param name="documentStorageFile"></param>
        /// <returns></returns>
        Task<AddDocumentResponse> UploadDocumentAsync(DocumentStorageFile documentStorageFile);

        /// <summary>
        /// This method is used to download document into storage location using Data Storage API.
        /// </summary>
        /// <param name="tenantId">Tenant Id of the logged-in user.</param>
        /// <param name="documentId">Document Id of the document.</param>
        /// <returns>returns DownloadDocumentResponse.</returns>
        Task<DownloadDocumentResponse> DownloadDocumentAsync(string tenantId, string documentId);
    }
}
