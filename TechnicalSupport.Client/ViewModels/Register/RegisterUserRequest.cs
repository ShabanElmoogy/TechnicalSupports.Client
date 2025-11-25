namespace TechnicalSupport.Client.ViewModels.Register;

public class RegisterUserRequest
{
    public string? Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public byte[]? ProfilePicture { get; set; }

    public string? Password { get; set; }

    public string? ConfirmPassword { get; set; }
}
