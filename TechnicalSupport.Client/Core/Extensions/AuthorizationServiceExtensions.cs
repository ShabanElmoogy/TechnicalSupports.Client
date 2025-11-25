namespace TechnicalSupport.Client.Core.Extensions;

public static class AuthorizationServiceExtensions
{
    public static async Task<bool> HasPermissionAsync(this IAuthorizationService service, ClaimsPrincipal user, string policy) =>
        (await service.AuthorizeAsync(user, null, policy)).Succeeded;
}