namespace TechnicalSupport.Client.Core.Handlers;

public class AuthenticationHandler : DelegatingHandler
{
    private readonly IAuthenticationServiceFactory _authenticationService;
    private readonly AuthenticationStateProvider _StateProvider;
    private readonly IHttpClientFactory _httpClientFactory;

    private bool _refreshing; //To avoid recursion in RefreshAsync

    public AuthenticationHandler(
        IAuthenticationServiceFactory authenticationService,
        AuthenticationStateProvider stateProvider,
        IHttpClientFactory httpClientFactory)
    {
        _authenticationService = authenticationService;
        _StateProvider = stateProvider;
        _httpClientFactory = httpClientFactory;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Authorization = null;
        request.Headers.Authorization = new AuthenticationHeaderValue(Strings.JwtSchema, string.Empty);

        var jwt = await _authenticationService.GetJwtAsync();

        if (!string.IsNullOrEmpty(jwt))
            request.Headers.Authorization = new AuthenticationHeaderValue(Strings.JwtSchema, jwt);

        var response = await base.SendAsync(request, cancellationToken);

        if (!_refreshing && !string.IsNullOrEmpty(jwt) && response.StatusCode == HttpStatusCode.Unauthorized)
        {
            try
            {
                _refreshing = true;

                if (await _authenticationService.RefreshAsync())
                {
                    jwt = await _authenticationService.GetJwtAsync();

                    if (!string.IsNullOrEmpty(jwt))
                        request.Headers.Authorization = new AuthenticationHeaderValue(Strings.JwtSchema, jwt);

                    ((AppAuthenticationStateProviderFactory)_StateProvider).NotifyUserAuthentication(jwt);
                    response = await base.SendAsync(request, cancellationToken);
                }
            }
            finally
            {
                _refreshing = false;
            }
        }

        return response;
    }
}
