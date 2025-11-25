namespace TechnicalSupport.Client.ViewModels.Companies;

public class SimpleCompanyViewModel
{
    public int Id { get; set; }

    public string NameAr { get; set; } = null!;
    public string NameEn { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public List<SimpleCompanyUserViewModel>? CompanyUsers;
}
