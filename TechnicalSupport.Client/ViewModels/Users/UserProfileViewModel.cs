namespace TechnicalSupport.Client.ViewModels.Users;

public class UserProfileViewModel
{
    public string Id { get; set; } = null!;
    
    public string UserName { get; set; } = null!;
   
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? ProfilePicture { get; set; }
}