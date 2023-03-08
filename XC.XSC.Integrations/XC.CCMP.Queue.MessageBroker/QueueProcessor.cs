using XC.CCMP.Queue.MessageAdapter;
using XC.XSC.ViewModels;

namespace XC.CCMP.Queue.Broker
{
    public class QueueProcessor : IQueueProcessor
    {
        private readonly IMapper _mapAdapter;

        public QueueProcessor(IMapper mapAdapter)
        {
            _mapAdapter = mapAdapter;
        }

        public async Task<bool> ProcessAsync(QueueMessage queueMessage)
        {
            this.GetTenantConfiguration(queueMessage.TenantId);

            return true;
        }

        private void GetTenantConfiguration(string tenantId)
        {
            //var config = _configurationService.GetConfigurationsAsync(tenantId).Result;

            //if (config.Configurations == null)
            //    throw new ArgumentNullException("storage configurations not found.");

            //var storageConfig = config.Configurations.Storages.Find(s => s.IsActive == true);

            //if (storageConfig == null)
            //    throw new ArgumentNullException("Active storage configuration not found.");

            //_mapAdapter.MapStorage(storageConfig);

        }
    }
}