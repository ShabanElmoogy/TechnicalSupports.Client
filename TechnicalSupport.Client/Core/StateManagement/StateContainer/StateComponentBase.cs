namespace TechnicalSupport.Client.Core.StateManagement.StateContainer;

using Microsoft.AspNetCore.Components;
public class StateComponentBase : ComponentBase, IDisposable
{
    [Inject]
    public StateContainer StateContainer { get; set; }

    protected override void OnInitialized()
    {
        StateContainer.UpdateSharedCompanies += async () => await InvokeAsync(() => StateHasChanged()); base.OnInitialized();
    }
    public void Dispose()
    {
        StateContainer.UpdateSharedCompanies -= async () => await InvokeAsync(() => StateHasChanged());
    }
}
