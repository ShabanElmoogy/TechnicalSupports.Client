namespace TechnicalSupport.Client.Core.Services.Dashboard;

public interface IDashboardSignalRService : IAsyncDisposable
{
    Task InitializeAsync(Func<string, object, Task> onDataUpdated);
    Task<bool> IsConnectedAsync();
}