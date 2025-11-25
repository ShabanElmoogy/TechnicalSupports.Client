namespace TechnicalSupport.Client.ViewModels.Roles;

public class RoleViewModel
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public bool IsDeleted { get; set; }

    public List<CheckBoxViewModel>? RoleClaims { get; set; } = [];
}
