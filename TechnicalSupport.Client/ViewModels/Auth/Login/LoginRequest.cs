namespace TechnicalSupport.Client.ViewModels.Auth.Login;

public class LoginRequest
{
    public string UserName { get; set; } = null!;

    public string PassWord { get; set; } = null!;

    public bool RememberMe { get; set; } = false;

}
