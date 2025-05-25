namespace dotnet_mvc.Services.Caching
{
    public interface IRedisCacheService
    {
        Task<T?> GetDataAsync<T>(string key);
        Task SetDataAsync<T>(string key, T data, TimeSpan? absoluteExpiration = null);
        Task RemoveDataAsync(string key);
        Task RemoveByPrefixAsync(string prefix);
    }
}