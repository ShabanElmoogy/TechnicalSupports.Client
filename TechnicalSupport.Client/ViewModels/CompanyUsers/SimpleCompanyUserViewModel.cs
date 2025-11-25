namespace TechnicalSupport.Client.ViewModels.CompanyUsers;

public class SimpleCompanyUserViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string AnyDesk { get; set; } = string.Empty;
    public string AnyDeskPassword { get; set; } = string.Empty;
    public string ServerPassword { get; set; } = string.Empty;
    public string CloudUser { get; set; } = string.Empty;
    public string CloudUserPassword { get; set; } = string.Empty;

}