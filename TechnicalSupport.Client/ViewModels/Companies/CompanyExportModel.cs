namespace TechnicalSupport.Client.ViewModels.Companies;

public class CompanyExportModel
{
    public int Id { get; set; }
    public string NameAr { get; set; } = null!;
    public string NameEn { get; set; } = null!;
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public int? ServerAddressId { get; set; }
    public SimpleServerAddressViewModel ServerDetail { get; set; }
}
