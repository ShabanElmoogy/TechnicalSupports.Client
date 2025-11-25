namespace TechnicalSupport.Client.Core.Services.AccountService;

public class AccountService : IAccountService
{
    private readonly HttpClient _httpClient;
    private readonly NavigationManager _navigationManager;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public AccountService(IHttpClientFactory httpClientFactory, NavigationManager navigationManager)
    {
        _httpClient = httpClientFactory.CreateClient(Strings.ApiClient);
        _navigationManager = navigationManager;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<UserProfileViewModel> GetUserInfo()
    {
        var response = await _httpClient.GetAsync("accountInfo/getInfo");

        if (!response.IsSuccessStatusCode)
        {
            HandleErrorResponse(response);
        }
        else
        {
            return await response.Content.ReadFromJsonAsync<UserProfileViewModel>();
        }

        return null;
    }

    public async Task<ApiResponse> UpdateUserInfo(UserProfileViewModel userProfile)
    {
        var request = await _httpClient.PutAsJsonAsync(ApiRoutes.AccountInfo.UpdateUserInfo, userProfile);

        if (request.StatusCode.Equals(401))
        {
            HandleErrorResponse(request);
        }
        else if (!request.IsSuccessStatusCode)
        {
            var resultMsg = await request.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(resultMsg, _jsonSerializerOptions);
            return new ApiResponse { Success = false, ErrorResponse = errorResponse };
        }
        return new ApiResponse { Success = true };
    }

    public async Task<ApiResponse> ChangePassword(ChangePasswordViewModel changePassword)
    {
        var request = await _httpClient.PutAsJsonAsync(ApiRoutes.AccountInfo.ChangePassword, changePassword);
        if (request.StatusCode.Equals(401))
        {
            HandleErrorResponse(request);
        }
        else if (!request.IsSuccessStatusCode)
        {
            var resultMsg = await request.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(resultMsg, _jsonSerializerOptions);
            return new ApiResponse { Success = false, ErrorResponse = errorResponse };
        }
        return new ApiResponse { Success = true };
    }

    private void HandleErrorResponse(HttpResponseMessage response)
    {
        _navigationManager.NavigateTo($"{InternalRoutes.ErrorPage}/{response.StatusCode}");
    }
}
