using XC.CCMP.Queue.MessageAdapter.AzureBlob;

namespace XC.CCMP.Queue.MessageAdapter
{
    public class StorageMapper: IMapper
    {
        public StorageMapper()
        {
            
        }

        //public async Task<bool> MapStorage(GetStorage tenatStorage)
        //{
        //    switch (tenatStorage.StorageType)
        //    { 
        //        case StorageType.AZ_Blob: 
        //            return this.BlobStorage(tenatStorage);                    

        //        case StorageType.AZ_Files:
        //            return false;// throw new ArgumentException("AZ_Files has not yet implemented");

        //        case StorageType.AZ_Managed_Disk:
        //            return false;// throw new ArgumentException("AZ_Managed_Disk storage has not yet implemented");

        //        case StorageType.Sharepoint:
        //            return false;// throw new ArgumentException("Sharepoint storage has not yet implemented");

        //        default:
        //            throw new ArgumentException("Storage type not found.");
        //    }
        //}

        //private bool BlobStorage(GetStorage tenatStorage)
        //{
        //    BlobFile blobFile = new BlobFile(new BlobConfig
        //    {
        //        ConnectionString = tenatStorage.ConnectionString,
        //        ContainerName = tenatStorage.ContainerName
        //    });
            
        //    // Here store document on Azure Blob

        //    //blobFile.Upload();

        //    return true;
        //}
    }
}