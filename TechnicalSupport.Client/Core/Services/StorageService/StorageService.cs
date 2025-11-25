namespace TechnicalSupport.Client.Core.Services.LocalStorageService;

public class StorageService(ILocalStorageService localStorage) : IStorageService
{
    private readonly ILocalStorageService _localStorage = localStorage;

    public async Task SetItemAsync<T>(string key, T value)
    {
        await _localStorage.SetItemAsync(key, value);
    }

    public async Task<T> GetItemAsync<T>(string key)
    {
        return await _localStorage.GetItemAsync<T>(key);
    }

    public async Task RemoveItemAsync(string key)
    {
        await _localStorage.RemoveItemAsync(key);
    }

    public async Task ClearAsync()
    {
        await _localStorage.ClearAsync();
    }

    public async Task AddToListAsync<T>(string key, T value)
    {
        var list = await _localStorage.GetItemAsync<List<T>>(key) ?? new List<T>();
        list.Add(value);
        await _localStorage.SetItemAsync(key, list);
    }

    public async Task RemoveFromListAsync<T>(string key, T value)
    {
        var list = await _localStorage.GetItemAsync<List<T>>(key);
        if (list != null)
        {
            list.Remove(value);
            await _localStorage.SetItemAsync(key, list);
        }
    }
}
