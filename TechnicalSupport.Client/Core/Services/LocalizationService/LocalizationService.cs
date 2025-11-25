namespace TechnicalSupport.Client.Core.Services.LocalizationService;

public class LocalizationService : ILocalizationService
{
    private readonly HttpClient _httpClient;
    private readonly NavigationManager _navigationManager;

    public LocalizationService(IHttpClientFactory httpClientFactory, NavigationManager navigationManager)
    {
        _httpClient = httpClientFactory.CreateClient(Strings.ApiClient);
        _navigationManager = navigationManager;
    }

    public async Task<Dictionary<string, string>> GetLocalizationData(string apiName, string language)
    {
        var apiEndpoint = $"{apiName}/{language}";
        try
        {
            var response = await _httpClient.GetFromJsonAsync<LocalizationResponse>(apiEndpoint);

            if (response != null && response.Success)
            {
                return response.Value;
            }
            else
            {
                return null;
            }
        }
        catch 
        {
            return null;
        }
    }

    public async Task<LocalizationResponse> SaveLocalization(string language, Dictionary<string, string> localizationData)
    {
        var apiEndpoint = $"api/localization/{language}"; // Adjust the endpoint path as necessary
        try
        {
            var response = await _httpClient.PostAsJsonAsync(apiEndpoint, localizationData);

            if (response.IsSuccessStatusCode)
            {
                return new LocalizationResponse { Success = true };
            }
            else
            {
                await HandleErrorResponseAsync(response);
                return new LocalizationResponse { Success = false, ErrorMessage = "Failed to save localization data." };
            }
        }
        catch (Exception ex)
        {
            return new LocalizationResponse { Success = false, ErrorMessage = ex.Message };
        }
    }

    public async Task<LocalizationResponse> UpdateLocalizationKey(string language, string key, string value)
    {
        var apiEndpoint = $"v1/api/Localization/UpdateLocalizationKey/{language}/{key}"; // Adjust the endpoint path as necessary
        try
        {
            var response = await _httpClient.PutAsJsonAsync(apiEndpoint, value);

            if (response.IsSuccessStatusCode)
            {
                return new LocalizationResponse { Success = true };
            }
            else
            {
                await HandleErrorResponseAsync(response);
                return new LocalizationResponse { Success = false, ErrorMessage = "Failed to update localization key." };
            }
        }
        catch (Exception ex)
        {
            return new LocalizationResponse { Success = false, ErrorMessage = ex.Message };
        }
    }

    private async Task HandleErrorResponseAsync(HttpResponseMessage response)
    {
        // Log error details or navigate to an error page
        _navigationManager.NavigateTo($"{InternalRoutes.ErrorPage}/{response.StatusCode}");
        var errorContent = await response.Content.ReadAsStringAsync();
        throw new Exception($"API error: {response.StatusCode} - {errorContent}");
    }
}
