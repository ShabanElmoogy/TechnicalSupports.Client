using Microsoft.JSInterop;
using TechnicalSupport.Client.Core.Services.ExportFilesService;

namespace TechnicalSupport.Client.Helpers;

public class ExportListService
{
    private readonly IExportFilesService _exportExcel;
    private readonly IJSRuntime _jSRuntime;

    public ExportListService(IExportFilesService exportExcel, IJSRuntime jSRuntime)
    {
        _exportExcel = exportExcel;
        _jSRuntime = jSRuntime;
    }

    private async Task ExportExcel(List<Dictionary<string, object>> data, string fileName, string culture)
    {
        var response = await _exportExcel.ExportFile(data, $"{ApiRoutes.v1}/{ApiRoutes.Export.ExportExcel}/{fileName}/{culture}");

        if (response.Success)
        {
            var fileContent = (byte[])response.Data!;
            await _jSRuntime.InvokeVoidAsync(JsFunctions.SaveAsFile, fileName, Convert.ToBase64String(fileContent));
        }
    }

    private async Task ExportCsv(List<Dictionary<string, object>> data, string fileName, string culture)
    {
        var response = await _exportExcel.ExportFile(data, $"{ApiRoutes.v1}/{ApiRoutes.Export.ExportCsv}/{fileName}/{culture}");

        if (response.Success)
        {
            var fileContent = (byte[])response.Data!;
            await _jSRuntime.InvokeVoidAsync(JsFunctions.SaveAsFile, fileName, Convert.ToBase64String(fileContent));
        }
    }

    private async Task ExportPdf(List<Dictionary<string, object>> data, string fileName, string reportHead, string culture)
    {
        var response = await _exportExcel.ExportFile(data, $"{ApiRoutes.v1}/{ApiRoutes.Export.ExportPdf}/{fileName}/{reportHead}/{culture}");

        if (response.Success)
        {
            var fileContent = (byte[])response.Data!;
            await _jSRuntime.InvokeVoidAsync(JsFunctions.SaveAsFile, fileName, Convert.ToBase64String(fileContent));
        }
    }

    public async Task ExportAsync(ExportType exportType, List<Dictionary<string, object>> gridSource, string filename, string reportHead, string culture)
    {
        switch (exportType)
        {
            case ExportType.Excel:
                await ExportExcel(gridSource, $"{filename}_{DateTime.Now:yyyy-MM-dd}.xlsx", culture);
                break;
            case ExportType.Csv:
                await ExportCsv(gridSource, $"{filename}_{DateTime.Now:yyyy-MM-dd}.csv", culture);
                break;
            case ExportType.Pdf:
                await ExportPdf(gridSource, $"{filename}_{DateTime.Now:yyyy-MM-dd}.pdf", reportHead, culture);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(exportType), exportType, "Unsupported export type.");
        }
    }
}

