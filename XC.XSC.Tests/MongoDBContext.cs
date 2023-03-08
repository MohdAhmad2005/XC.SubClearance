
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using XC.CCMP.KeyVault;

namespace XC.XSC.Tests
{

    public class TestMongoContext
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
       
        public TestMongoContext(IKeyVaultConfig keyVaultConfig)
        {
            _client = new MongoClient(keyVaultConfig.MongoConnectionString);
            _database = _client.GetDatabase("UnitTestTempDatabase");
        }

        public IMongoClient Client => _client;
        public IMongoDatabase Database => _database;

    }
}
