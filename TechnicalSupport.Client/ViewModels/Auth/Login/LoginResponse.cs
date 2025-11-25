namespace TechnicalSupport.Client.ViewModels.Auth.Login;

public class LoginResponse
{
    public string Token { get; set; } = null!;
    public DateTime TokenExpiration { get; set; }
    public string RefreshToken { get; set; } = null!;
    public DateTime RefreshTokenExpiration { get; set; }
}


