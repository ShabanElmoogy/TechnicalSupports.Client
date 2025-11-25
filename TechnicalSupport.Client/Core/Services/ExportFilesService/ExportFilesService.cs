using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Text.Json;

namespace TechnicalSupport.Client.Core.Services.ExportFilesService;

public class ExportFilesService : IExportFilesService
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    public NavigationManager _navigationManager;
    private readonly HttpClient _httpClient;

    public ExportFilesService(NavigationManager navigationManager, IHttpClientFactory httpClientFactory)
    {
        _navigationManager = navigationManager;
        _httpClient = httpClientFactory.CreateClient(Strings.ApiClient);
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            ReferenceHandler = ReferenceHandler.Preserve,
            MaxDepth = 10
        };
    }


    public async Task<ApiResponse<byte[]>> ExportFile(List<Dictionary<string, object>> data, string apiName)
    {
        // Send the data as a POST request to the API
        var request = await _httpClient.PostAsJsonAsync(apiName, data);

        // Handle Unauthorized error
        if (request.StatusCode == HttpStatusCode.Unauthorized)
        {
            HandleErrorResponse(request);
        }
        // Handle other errors
        else if (!request.IsSuccessStatusCode)
        {
            var resultMsg = await request.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(resultMsg, _jsonSerializerOptions);
            return new ApiResponse<byte[]> { Success = false, ErrorResponse = errorResponse };
        }

        // If the request is successful, get the file content (Excel file)
        var fileContent = await request.Content.ReadAsByteArrayAsync();

        // Return the file content as a success response
        return new ApiResponse<byte[]> { Success = true, Data = fileContent };
    }

    private void HandleErrorResponse(HttpResponseMessage response)
    {
        _navigationManager.NavigateTo($"{InternalRoutes.ErrorPage}/{response.StatusCode}");
    }
}
