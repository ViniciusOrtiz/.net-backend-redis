using Backend.Application.Contracts.Services.Caching;
using StackExchange.Redis;

namespace Backend.Infrastructure.Services.Caching
{
    public class RedisCachingService : ICachingService
    {
        private ConnectionMultiplexer _redisConnection;
        private IDatabase _cache;
        private readonly string _connectionString;

        public RedisCachingService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Connect()
        {
            _redisConnection = ConnectionMultiplexer.Connect(_connectionString);
            _cache = _redisConnection.GetDatabase();
        }

        public void Disconnect()
        {
            if(_redisConnection is not null)
            {
                _redisConnection.Close();
            }
        }

        public void Create(string key, string value, TimeSpan? expiry = null)
        {
            _cache.StringSet(key, value, expiry);
        }

        public void Delete(string key)
        {
            _cache.KeyDelete(key);
        }

        public string Read(string key)
        {
            return _cache.StringGet(key);
        }

        public IEnumerable<string> Scan(string prefix)
        {
            int cursor = 0;
            var keys = _redisConnection.GetServer(_redisConnection.GetEndPoints()[0]).Keys(pattern: $"{prefix}*").Select(k => (string)k);

            foreach (var key in keys)
            {
                yield return key;
            }
        }

        public void Update(string key, string value, TimeSpan? expiry = null)
        {
            if (_cache.KeyExists(key))
            {
                _cache.StringSet(key, value, expiry);
            }
        }

    }
}
