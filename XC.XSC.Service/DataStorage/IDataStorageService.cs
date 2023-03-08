using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.CCMP.DataStorage.Models.Response;

namespace XC.XSC.Service.DataStorage
{
    /// <summary>
    /// This interface will mention all the document related services.
    /// </summary>
    public interface IDataStorageService
    {
        /// <summary>
        /// This method is used to download document into storage location using Data Storage API.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="documentId"></param>
        /// <returns>DownloadDocumentResponse model.</returns>
        Task<DownloadDocumentResponse> DownloadDocumentAsync(string tenantId, string documentId);
    }
}
