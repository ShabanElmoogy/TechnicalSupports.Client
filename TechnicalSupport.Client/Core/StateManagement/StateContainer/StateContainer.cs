using TechnicalSupport.Client.ViewModels.Companies;

namespace TechnicalSupport.Client.Core.StateManagement.StateContainer;

public class StateContainer
{

    private List<CompanyViewModel> _sharedCompanies = [];

    public List<CompanyViewModel> SharedCompanies
    {
        get { return _sharedCompanies; }
        set
        {
            _sharedCompanies = value;
            UpdateSharedCompanies?.Invoke();
        }
    }

    public event Func<Task>? UpdateSharedCompanies;
}

