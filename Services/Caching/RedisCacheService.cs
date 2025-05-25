using System.Text.Json;
using dotnet_mvc.Services.Caching;
using Microsoft.Extensions.Caching.Distributed;
namespace dotnet_mvc.Services.Caching
{

    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache _cache;
        private static readonly HashSet<string> _allKeys = new(); // In-memory fallback; consider storing keys in Redis instead for scale

        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T?> GetDataAsync<T>(string key)
        {
            var data = await _cache.GetStringAsync(key);
            if (data == null) return default;
            try
            {
                return JsonSerializer.Deserialize<T>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch
            {
                return default;
            }
        }

        public async Task SetDataAsync<T>(string key, T data, TimeSpan? absoluteExpiration = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpiration ?? TimeSpan.FromMinutes(5)
            };

            _allKeys.Add(key);
            await _cache.SetStringAsync(key, JsonSerializer.Serialize(data), options);
        }

        public async Task RemoveDataAsync(string key)
        {
            _allKeys.Remove(key);
            await _cache.RemoveAsync(key);
        }

        public async Task RemoveByPrefixAsync(string prefix)
        {
            var keysToRemove = _allKeys.Where(k => k.StartsWith(prefix)).ToList();
            foreach (var key in keysToRemove)
            {
                await _cache.RemoveAsync(key);
                _allKeys.Remove(key);
            }
        }
    }
}