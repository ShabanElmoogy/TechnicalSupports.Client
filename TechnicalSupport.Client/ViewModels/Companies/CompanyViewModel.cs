namespace TechnicalSupport.Client.ViewModels.Companies;

public class CompanyViewModel
{
    public int Id { get; set; }
    public string NameAr { get; set; }
    public string NameEn { get; set; }

    private int? _serverAddressId;

    public int? ServerAddressId
    {
        get => _serverAddressId.HasValue && _serverAddressId > 0 ? _serverAddressId : null;
        set => _serverAddressId = value.HasValue && value > 0 ? value : null;
    }

    public SimpleServerAddressViewModel ServerDetail { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public bool IsDeleted { get; set; }
}
