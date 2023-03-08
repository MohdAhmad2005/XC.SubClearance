
namespace XC.CCMP.Caching.Redis
{
    public class RedisCache : IRedisCache
    {
        private readonly Task<RedisConnection> _redisConnectionFactory;
        private RedisConnection _redisConnection;

        public RedisCache(Task<RedisConnection> redisConnectionFactory)
        {
            _redisConnectionFactory = redisConnectionFactory;            
        }

        public async Task<bool>  SetValue(string key, string value)
        {
            _redisConnection = await _redisConnectionFactory;

            (await _redisConnection.BasicRetryAsync(async (db) => await db.StringSetAsync(key, value))).ToString();

            return true;
        }

        public async Task<string> GetValue(string key) {
            
            _redisConnection = await _redisConnectionFactory;

            return (await _redisConnection.BasicRetryAsync(async (db) => await db.StringGetAsync(key))).ToString();  
        }

        //public async Task<T> GetValue<T>(string key)
        //{
        //    Type type = typeof(T);

        //    _redisConnection = await _redisConnectionFactory;

        //    return (await _redisConnection.BasicRetryAsync(async (db) => await db.StringGetAsync(key))).ToString();
        //}
    }
}
