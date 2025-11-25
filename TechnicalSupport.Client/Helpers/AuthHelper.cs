using JwtSecurityTokenHandler = System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler;

namespace TechnicalSupport.Client.Helpers;

public static class AuthHelper
{
    public static IEnumerable<Claim> GetClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.ReadToken(jwt) as JwtSecurityToken;
        claims = securityToken?.Claims.ToList();
        return claims;
    }
}
