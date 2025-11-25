using TechnicalSupport.Client.ViewModels.Companies;

namespace TechnicalSupport.Client.ViewModels.CompanyUsers;
public class CompanyUserExportModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CompanyId { get; set; }
    public string AnyDesk { get; set; } = string.Empty;
    public string AnyDeskPassword { get; set; } = string.Empty;
    public string ServerPassword { get; set; } = string.Empty;
    public string CloudUser { get; set; } = string.Empty;
    public string CloudUserPassword { get; set; } = string.Empty;
    public SimpleCompanyViewModel? CompanyDetail { get; set; }
}
