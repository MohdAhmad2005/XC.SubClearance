
namespace XC.CCMP.Queue.MessageAdapter.AzureBlob
{
    internal interface IBlob : IDisposable
    {
        bool Upload(Stream FileStream, string FileName, string uniqueFileName);
        Stream Download(string uniqueFileName);
        bool Delete(string uniqueFileName);
        string GetUniqueueFileName(string ActualFileName);
    }
}
