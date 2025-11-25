using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace TechnicalSupport.Client.Core.Services.StructService;

public class DashboardService : IDashboardService
{
    private readonly HttpClient _httpClient;
    public NavigationManager _navigationManager;

    public DashboardService(IHttpClientFactory httpClientFactory, NavigationManager navigationManager)
    {
        _httpClient = httpClientFactory.CreateClient(Strings.ApiClient);
        _navigationManager = navigationManager;
    }

    public async Task<int> GetInfo(string apiName)
    {
        var response = await _httpClient.GetAsync(apiName);

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = await response.Content.ReadAsStringAsync();
            throw new Exception($"Error: {response.ReasonPhrase}. Details: {errorMessage}");
        }

        var content = await response.Content.ReadAsStringAsync();

        try
        {
            // Deserialize into a dynamic object to access "count"
            var jsonResponse = JsonConvert.DeserializeObject<dynamic>(content);
            int count = 0; 
            if (jsonResponse != null && int.TryParse((string?)jsonResponse.count, out count))
            {
                return count;
            }

            throw new Exception($"Invalid response format. Content: {content}");
        }
        catch (Newtonsoft.Json.JsonException ex)
        {
            throw new Exception($"Failed to parse API response. Content: {content}", ex);
        }
    }

    private void HandleErrorResponse(HttpResponseMessage response)
    {
        _navigationManager.NavigateTo($"{InternalRoutes.ErrorPage}/{response.StatusCode}");
    }
}
