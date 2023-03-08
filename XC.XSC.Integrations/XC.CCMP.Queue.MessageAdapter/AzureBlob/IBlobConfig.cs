namespace XC.CCMP.Queue.MessageAdapter.AzureBlob
{
    public interface IBlobConfig: IConfig
    {
        //public string AllowedHosts { get; set; }
        //public string ConnectionString { get; set; }
        public string ContainerName { get; set; }
    }
}
