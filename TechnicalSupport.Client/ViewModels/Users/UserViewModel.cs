namespace TechnicalSupport.Client.ViewModels.Users;

public class UserViewModel 
{
    public string? Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? UserName { get; set; }

    public string Email { get; set; }

    public string? Password { get; set; }

    public bool IsDisabled { get; set; }

    public string CreatedById { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatedByPc { get; set; }

    public string? UpdatedById { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? UpdatedByPc { get; set; }

    public string? DeletedById { get; set; }

    public DateTime? DeletedOn { get; set; }

    public string? DeletedByPc { get; set; }

    public List<string> Roles { get; set; } = [];
}
