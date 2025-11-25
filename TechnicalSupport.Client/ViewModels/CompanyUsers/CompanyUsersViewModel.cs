namespace TechnicalSupport.Client.ViewModels.CompanyUsers;
public class CompanyUsersViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int CompanyId { get; set; }

    public string AnyDesk { get; set; } = string.Empty;

    public string AnyDeskPassword { get; set; } = string.Empty;

    public string ServerPassword { get; set; } = string.Empty;

    public string CloudUser { get; set; } = string.Empty;

    public string CloudUserPassword { get; set; } = string.Empty;

    public bool IsDeleted { get; set; }

    public SimpleCompanyViewModel? CompanyDetail { get; set; }
}
