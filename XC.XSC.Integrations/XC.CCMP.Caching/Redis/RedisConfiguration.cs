namespace XC.CCMP.ViewModels.Redis
{
    public class RedisConfiguration : IRedisConfiguration
    {
        public string ConnectionString { get; set; }
    }

    public interface IRedisConfiguration
    {
        public string ConnectionString { get; set; }
    }
}
