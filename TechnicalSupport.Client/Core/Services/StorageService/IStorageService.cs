namespace TechnicalSupport.Client.Core.Services.LocalStorageService;
public interface IStorageService
{
    Task SetItemAsync<T>(string key, T value);
    Task<T> GetItemAsync<T>(string key);
    Task RemoveItemAsync(string key);
    Task ClearAsync();
    Task AddToListAsync<T>(string key, T value);
    Task RemoveFromListAsync<T>(string key, T value);
}
