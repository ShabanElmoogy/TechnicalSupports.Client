namespace TechnicalSupport.Client.Identity.Services;

public class AppAuthenticationStateProviderFactory : AuthenticationStateProvider
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly AuthenticationState _anonymous;
    private readonly ILocalStorageService _localStorageService;

    public AppAuthenticationStateProviderFactory(IHttpClientFactory httpClientFactory, ILocalStorageService localStorageService)
    {
        _httpClientFactory = httpClientFactory;
        _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        _localStorageService = localStorageService;
    }

    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var isPersistant = await _localStorageService.GetItemAsStringAsync("IsPersistentToken");
        if (isPersistant != "IsPersistent")
        {
            await _localStorageService.RemoveItemAsync("IsPersistentToken");
        }

        var token = await _localStorageService.GetItemAsync<string>("_token");

        if (string.IsNullOrEmpty(token))
            return _anonymous;

        var client = _httpClientFactory.CreateClient("ApiClient");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(AuthHelper.GetClaimsFromJwt(token), "jwtAuthType")));
    }

    public void NotifyUserAuthentication(string jwtToken)
    {
        var authenticationUser = new ClaimsPrincipal(new ClaimsIdentity(AuthHelper.GetClaimsFromJwt(jwtToken), "jwtAuthType"));
        var authState = Task.FromResult(new AuthenticationState(authenticationUser));
        NotifyAuthenticationStateChanged(authState);
    }

    public void NotifyUserLogout()
    {
        var authState = Task.FromResult(_anonymous);
        NotifyAuthenticationStateChanged(authState);
    }
}
