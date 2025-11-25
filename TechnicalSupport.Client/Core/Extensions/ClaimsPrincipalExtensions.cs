namespace TechnicalSupport.Client.Core.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetEmail(this ClaimsPrincipal principal)
      => principal.FindFirstValue(ClaimTypes.Email);

    public static string? GetUserId(this ClaimsPrincipal principal)
  => principal.FindFirstValue(ClaimTypes.NameIdentifier);

    public static string? GetUserName(this ClaimsPrincipal principal)
  => principal.FindFirstValue(ClaimTypes.Name);

    private static string? FindFirstValue(this ClaimsPrincipal principal, string claimType) =>
    principal is null
        ? throw new ArgumentNullException(nameof(principal))
        : principal.FindFirst(claimType)?.Value;
}
