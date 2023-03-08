namespace XC.CCMP.Queue.MessageAdapter.AzureBlob
{
    public class BlobConfig: IBlobConfig
    {
        public string ConnectionString { get; set; }
        public string ContainerName { get; set; }
    }
}
