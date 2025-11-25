namespace TechnicalSupport.Client.Identity.Services;

public class UserInfoService(AuthenticationStateProvider stateProvider)
{
    private readonly AuthenticationStateProvider _stateProvider = stateProvider;

    public async Task<string> GetUserIdAsync()
    {
        var authState = await _stateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        return user.GetUserId();
    }

    public async Task<string> GetUserNameAsync()
    {
        var authState = await _stateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        return user!.GetUserName();
    }

    public async Task<string> GetUserEmailAsync()
    {
        var authState = await _stateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        return user.GetEmail();
    }

    public async Task<ClaimsPrincipal> GetUserAsync()
    {
        var authState = await _stateProvider.GetAuthenticationStateAsync();
        return authState.User;
    }
}
