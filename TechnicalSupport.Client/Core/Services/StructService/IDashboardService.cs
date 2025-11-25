namespace TechnicalSupport.Client.Core.Services.StructService;

public interface IDashboardService
{
    Task<int> GetInfo(string ApiName);
}
