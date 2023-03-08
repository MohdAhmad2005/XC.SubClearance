using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace XC.CCMP.Queue.MessageAdapter.AzureBlob
{
    internal class BlobFile : IBlob
    {
        private IBlobConfig _azureBlobConfig;// = null;
        private BlobContainerClient _container;// = null;
        private bool disposedValue;
        private Stream _stream = new MemoryStream();

        public BlobFile(IBlobConfig azureBlobConfig)
        {
            _azureBlobConfig = azureBlobConfig;
            
            _container = new BlobContainerClient(_azureBlobConfig.ConnectionString, _azureBlobConfig.ContainerName);
        }

        public bool Upload(Stream FileStream, string FileName, string uniqueFileName)
        {
            // Get a reference to a blob named "sample-file" in a container named "sample-container"
            BlobClient blob = _container.GetBlobClient(uniqueFileName);

            FileStream.Position = 0;
            // Upload local file
            blob.Upload(FileStream);

            return true;
        }

        public Stream Download(string uniqueFileName)
        {
            // Get a reference to a blob named {uniqueFileName} in a container
            BlobClient blob = _container.GetBlobClient(uniqueFileName);

            blob.DownloadTo(_stream);

            return _stream;
        }

        public bool Delete(string uniqueFileName)
        {
            try
            {
                // Get a reference to a blob named {uniqueFileName} in a container
                BlobClient blob = _container.GetBlobClient(uniqueFileName);

                blob.Delete();
            }
            catch (RequestFailedException ex) when (ex.ErrorCode == BlobErrorCode.ContainerBeingDeleted || ex.ErrorCode == BlobErrorCode.ContainerNotFound)
            {
                // Ignore any errors if the container being deleted or if it has already been deleted
            }

            return true;
        }

        public string GetUniqueueFileName(string ActualFileName)
        {
            return ActualFileName + "_" + DateTime.Now.ToString("yyyyddMMHHmmssffff");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)

                    if (this._azureBlobConfig != null)
                        this._azureBlobConfig = null;

                    if (_stream != null)
                        _stream.Dispose();

                    if (_container != null)
                        _container = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
