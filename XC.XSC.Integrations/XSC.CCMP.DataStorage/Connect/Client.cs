using Newtonsoft.Json;
using XC.CCMP.DataStorage.Models.Request;
using XC.CCMP.DataStorage.Models.Response;

namespace XC.CCMP.DataStorage.Connect
{
    public class Client : IClient
    {
        private readonly IApiConfig _apiConfig;

        public Client(IApiConfig apiConfig)
        {
            _apiConfig = apiConfig;
        }

        /// <summary>
        /// This method is used to upload document into storage location using Data Storage API.
        /// </summary>
        /// <param name="documentStorageFile"></param>
        /// <returns></returns>
        public async Task<AddDocumentResponse> UploadDocumentAsync(DocumentStorageFile documentStorageFile)
        {
            AddDocumentResponse addDocumentResponse = new AddDocumentResponse();
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new(_apiConfig.UploadEndpoint)
                };

                var content = new MultipartFormDataContent();
                content.Add(new StringContent(documentStorageFile.TenantId), "TenantId");
                content.Add(new StringContent("EmailScheduler"), "UploadedBy");
                content.Add(new StringContent(documentStorageFile.Path), "fileGroup.Path");

                content.Add(new StreamContent(documentStorageFile.StreamContent), $"FileGroup.FileGroup", documentStorageFile.FileName);

                using var request = new HttpRequestMessage(HttpMethod.Post, "UploadDocument");

                request.Content = content;

                using var httpResponseMessage = client.Send(request);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var response = await httpResponseMessage.Content.ReadAsStringAsync();
                    addDocumentResponse = JsonConvert.DeserializeObject<AddDocumentResponse>(response);
                }
            }
            catch (Exception ex)
            {

            }
            return addDocumentResponse;
        }

        /// <summary>
        /// This method is used to download document into storage location using Data Storage API.
        /// </summary>
        /// <param name="tenantId">Tenant Id of the logged-in user.</param>
        /// <param name="documentId">Document Id of the document.</param>
        /// <returns>Returns DownloadDocumentResponse.</returns>
        public async Task<DownloadDocumentResponse> DownloadDocumentAsync(string tenantId, string documentId)
        {
            try
            {
                DownloadDocumentResponse downloadDocumentResponse = new DownloadDocumentResponse();
                using (HttpClient client = new HttpClient())
                {
                    string url = $"{_apiConfig.DownloadEndpoint}/{tenantId}/{documentId}";
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        downloadDocumentResponse.StreamData = await response.Content.ReadAsStreamAsync();
                        if(response.Content.Headers.ContentDisposition != null)
                        {
                            downloadDocumentResponse.FileName = response.Content.Headers.ContentDisposition.FileName.TrimStart('\"').TrimEnd('\"');
                        }
                        return downloadDocumentResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                return new DownloadDocumentResponse();
            }
            return new DownloadDocumentResponse();

        }
    }    
}
