
namespace XC.CCMP.DataStorage.Models.Response
{
    /// <summary>
    /// Download document response model.
    /// </summary>
    public class DownloadDocumentResponse
    {
        /// <summary>
        /// file in a stream data format.
        /// </summary>
        public Stream? StreamData { get; set; }

        /// <summary>
        /// file name.
        /// </summary>
        public string? FileName { get; set; }
    }
}
