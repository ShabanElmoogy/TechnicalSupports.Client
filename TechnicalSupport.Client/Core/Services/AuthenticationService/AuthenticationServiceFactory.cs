using TechnicalSupport.Client.Core.Services.LocalStorageService;

namespace TechnicalSupport.Client.Core.Services.AuthenticationService;

public class AuthenticationServiceFactory : IAuthenticationServiceFactory
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly AuthenticationStateProvider _StateProvider;
    private readonly IHttpClientFactory _httpClient;
    private readonly NavigationManager _navigationManager;
    private readonly IStorageService _storageService;
    public event Action<string?>? LoginChange;

    private const string _token = nameof(_token);
    private const string _refreshToken = nameof(_refreshToken);
    private string? _jwtCache;

    public AuthenticationServiceFactory(
        AuthenticationStateProvider stateProvider,
        IHttpClientFactory httpClientFactory,
        NavigationManager navigationManager,
        IStorageService storageService)
    {
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _StateProvider = stateProvider;
        _httpClient = httpClientFactory;
        _navigationManager = navigationManager;
        _storageService = storageService;
    }

    public async ValueTask<string> GetJwtAsync()
    {
        if (string.IsNullOrEmpty(_jwtCache))
            _jwtCache = await _storageService.GetItemAsync<string>(_token);

        return _jwtCache;
    }

    private string GetUserIdFromToken(string token)
    {
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
        var userIdClaim = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        return userIdClaim.Value;
    }

    private HttpClient CreateClient()
    {
        var client = _httpClient.CreateClient(Strings.ApiClient);
        client.DefaultRequestHeaders.Authorization = null;
        return client;
    }

    private static string GetUsername(string token)
    {
        var jwt = new JwtSecurityToken(token);
        return jwt.Claims.First(c => c.Type == ClaimTypes.Name).Value;
    }

    public async Task<ApiResponse> RegisterUser(RegisterUserRequest userRegistration, string returnUrl)
    {
        var client = CreateClient();
        var apiUrl = $"{ApiRoutes.v1}/{ApiRoutes.Auth.Register}";

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, apiUrl)
        {
            Content = JsonContent.Create(userRegistration)
        };

        requestMessage.Headers.Add(Strings.ReturnUrl, returnUrl);

        var result = await client.SendAsync(requestMessage);

        if (!result.IsSuccessStatusCode)
        {
            var resultMsg = await result.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(resultMsg, _jsonSerializerOptions);
            return new ApiResponse { Success = false, ErrorResponse = errorResponse };
        }

        return new ApiResponse { Success = true };
    }

    public async Task<ApiResponse> ConfirmEmail(ConfirmEmailRequest request)
    {
        var client = CreateClient();

        var result = await client.PostAsJsonAsync($"{ApiRoutes.v1}/{ApiRoutes.Auth.ConfirmEmail}", request);
        if (!result.IsSuccessStatusCode)
        {
            var resultMsg = await result.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(resultMsg, _jsonSerializerOptions);
            return new ApiResponse { Success = false, ErrorResponse = errorResponse };
        }
        return new ApiResponse { Success = true };
    }

    public async Task<ApiResponse> ResendConfirmEmail(ResendConfirmationEmailRequest request, string returnUrl)
    {
        var client = CreateClient();

        var apiUrl = $"{ApiRoutes.v1}/{ApiRoutes.Auth.ResendConfirmationEmailAsync}";

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, apiUrl)
        {
            Content = JsonContent.Create(request)
        };

        requestMessage.Headers.Add(Strings.ReturnUrl, returnUrl);

        var result = await client.SendAsync(requestMessage);

        if (!result.IsSuccessStatusCode)
        {
            var resultMsg = await result.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(resultMsg, _jsonSerializerOptions);
            return new ApiResponse { Success = false, ErrorResponse = errorResponse };
        }
        return new ApiResponse { Success = true };
    }

    public async Task<ApiResponse<LoginResponse>> Login(LoginRequest login)
    {
        var client = CreateClient();

        var result = await client.PostAsJsonAsync($"{ApiRoutes.v1}/{ApiRoutes.Auth.Login}", login);

        var resultMsg = await result.Content.ReadAsStringAsync();

        if (result.StatusCode != HttpStatusCode.OK)
        {
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(resultMsg, _jsonSerializerOptions);
            return new ApiResponse<LoginResponse> { Success = false, ErrorResponse = errorResponse };
        }

        var successResponse = JsonSerializer.Deserialize<LoginResponse>(resultMsg, _jsonSerializerOptions);

        // Store the new tokens
        await _storageService.SetItemAsync(_token, successResponse.Token);
        await _storageService.SetItemAsync(_refreshToken, successResponse.RefreshToken);

        // Notify user authentication state
        ((AppAuthenticationStateProviderFactory)_StateProvider).NotifyUserAuthentication(successResponse.Token);

        // Set the new token in the Authorization header
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Strings.JwtSchema, successResponse.Token);

        // Manage persistent login state
        if (login.RememberMe)
            await _storageService.SetItemAsync(Strings.IsPersistentToken, Strings.IsPersistent);
        else
            await _storageService.RemoveItemAsync(Strings.IsPersistentToken);

        // Trigger login change event
        LoginChange?.Invoke(GetUsername(successResponse.Token));

        return new ApiResponse<LoginResponse> { Success = true, Entity = successResponse };
    }
    public async Task<UserPhoto> GetUserPhoto(string userId)
    {
        var client = CreateClient();

        if (userId.IsNotEmpty())
        {
            var result = await client.GetAsync($"{ApiRoutes.v1}/{ApiRoutes.Users.GetUserPhoto}/{userId}");
            var resultMsg = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
            {
                var response = JsonSerializer.Deserialize<UserPhoto>(resultMsg, _jsonSerializerOptions);
                return new UserPhoto { ProfilePicture = response.ProfilePicture };
            }
        }
        return new UserPhoto();
    }

    public async Task LogoutAsync()
    {
        try
        {
            // Retrieve the token from storage
            var token = await _storageService.GetItemAsync<string>(_token);

            if (!string.IsNullOrEmpty(token))
            {
                // Extract the user ID from the token
                var userId = GetUserIdFromToken(token);

                // Perform the logout API call
                var client = CreateClient();
                await client.PostAsJsonAsync($"{ApiRoutes.v1}/{ApiRoutes.Auth.LogOut}?userId={userId}", new { });
            }

            // Clear token and refresh token from storage
            await _storageService.RemoveItemAsync(_token);
            await _storageService.RemoveItemAsync(_refreshToken);

            _jwtCache = null;

            // Clear the HttpClient Authorization header
            _navigationManager.NavigateTo(InternalRoutes.Login, forceLoad: true);
            ((AppAuthenticationStateProviderFactory)_StateProvider).NotifyUserLogout();

            // Notify listeners of login state change
            LoginChange?.Invoke(null);
        }
        catch
        {
            // Optionally log or handle the exception
        }
    }

    public async Task<bool> RefreshAsync()
    {
        var client = CreateClient();

        var refreshRequest = new RefreshTokenRequest
        {
            Token = await _storageService.GetItemAsync<string>(_token),
            RefreshToken = await _storageService.GetItemAsync<string>(_refreshToken)
        };

        var response = await client.PostAsJsonAsync($"{ApiRoutes.v1}/{ApiRoutes.Auth.RefreshToken}", refreshRequest);

        if (!response.IsSuccessStatusCode)
        {
            _navigationManager.NavigateTo(InternalRoutes.Logout);

            return false;
        }

        var content = await response.Content.ReadFromJsonAsync<LoginResponse>();

        if (content == null)
            throw new InvalidDataException();

        await _storageService.SetItemAsync(_token, content.Token);
        await _storageService.SetItemAsync(_refreshToken, content.RefreshToken);

        _jwtCache = content.Token;

        return true;
    }

    public async Task<ApiResponse> ForgetPassword(ForgorPasswordViewModel forgorPasswordViewModel, string returnUrl)
    {
        var client = CreateClient();

        var apiUrl = $"{ApiRoutes.v1}/{ApiRoutes.Auth.ForgetPassword}";

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, apiUrl)
        {
            Content = JsonContent.Create(forgorPasswordViewModel)
        };

        // Add the return URL to the headers
        requestMessage.Headers.Add(Strings.ReturnUrl, returnUrl);

        var result = await client.SendAsync(requestMessage);

        if (!result.IsSuccessStatusCode)
        {
            var resultMsg = await result.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(resultMsg, _jsonSerializerOptions);
            return new ApiResponse { Success = false, ErrorResponse = errorResponse };
        }

        return new ApiResponse { Success = true };
    }

    public async Task<ApiResponse> ResetPassword(ResetPasswordRequest resetPasswordRequest)
    {
        var client = CreateClient();

        var result = await client.PostAsJsonAsync($"{ApiRoutes.v1}/{ApiRoutes.Auth.ResetPassword}", resetPasswordRequest);
        if (!result.IsSuccessStatusCode)
        {
            var resultMsg = await result.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(resultMsg, _jsonSerializerOptions);
            return new ApiResponse { Success = false, ErrorResponse = errorResponse };
        }
        return new ApiResponse { Success = true };
    }

    public async Task OpenHangfire()
    {
        var client = CreateClient();
        await client.PostAsync(ApiRoutes.Auth.HangFire, null);
    }
}