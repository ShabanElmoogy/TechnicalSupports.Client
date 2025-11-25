namespace TechnicalSupport.Client.ViewModels.ServersAddress;

public class ServersAddressViewModel
{
    public int Id { get; set; }

    public string? Address { get; set; }

    public bool IsDeleted { get; set; }

    public List<SimpleCompanyViewModel>? Companies { get; set; }
}
