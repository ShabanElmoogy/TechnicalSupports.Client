namespace TechnicalSupport.Client.Core.Services.ExportFilesService;

public interface IExportFilesService
{
    Task<ApiResponse<byte[]>> ExportFile(List<Dictionary<string, object>> data, string apiName);
}
