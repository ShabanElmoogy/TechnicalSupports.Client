namespace TechnicalSupport.Client.ViewModels.Users;
public class UserExportViewModel
{
    public string? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public bool IsDisabled { get; set; }
    public string Roles { get; set; }
}
