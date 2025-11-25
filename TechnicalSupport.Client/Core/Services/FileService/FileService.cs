namespace TechnicalSupport.Client.Core.Services.FileService;

public class FileService : IFileService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly IJSRuntime _jSRuntime;

    public FileService(IHttpClientFactory httpClientFactory, IJSRuntime jSRuntime)
    {
        _httpClient = httpClientFactory.CreateClient(Strings.ApiClient);
        _jSRuntime = jSRuntime;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true, // This allows case-insensitive matching of JSON properties.
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull // Ignore properties that are null when serializing.
        };
    }

    public async Task<ApiResponse> UploadFileAsync(InputFileChangeEventArgs e, string uploadUrl, long maxFileSize = int.MaxValue)
    {
        var response = new ApiResponse<bool>();

        // Ensure that a file is selected
        var file = e.File ?? throw new ArgumentException("No file selected for upload.");

        using var content = new MultipartFormDataContent();

        var fileContent = new StreamContent(file.OpenReadStream(maxFileSize));

        try
        {
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
        }
        catch (Exception)
        {
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        }

        content.Add(
            content: fileContent,
            name: "\"file\"",  // Assuming the server expects a field named "file"
            fileName: file.Name);

        var httpResponse = await _httpClient.PostAsync(uploadUrl, content);

        if (!httpResponse.IsSuccessStatusCode)
        {
            var resultMsg = await httpResponse.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(resultMsg, _jsonSerializerOptions);
            return new ApiResponse<bool> { Success = false, ErrorResponse = errorResponse };
        }

        return new ApiResponse<bool> { Success = true };
    }

    public async Task<ApiResponse> DownloadFileAsync(string downloadUrl, string storedFileName, string fileName)
    {
        var response = await _httpClient.GetAsync($"{downloadUrl}/{storedFileName}");

        if (response.IsSuccessStatusCode)
        {
            // Convert the byte array to a stream
            // Read file content as byte array
            var fileBytes = await response.Content.ReadAsByteArrayAsync();

            // Convert the byte array to a stream
            var memoryStream = new MemoryStream(fileBytes);
            var streamRef = new DotNetStreamReference(memoryStream);

            await _jSRuntime.InvokeVoidAsync(JsFunctions.DownloadFileFromStream, fileName, streamRef);

            return new ApiResponse { Success = true };
        }
        else
        {
            // Log the error response for debugging
            var resultMsg = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(resultMsg, _jsonSerializerOptions);
            return new ApiResponse { Success = false, ErrorResponse = errorResponse };
        }
    }

    public async Task<ApiResponse> DownLoadStream(string downloadUrl, Guid id)
    {
        var response = await _httpClient.GetAsync($"{downloadUrl}/{id}");

        if (response.IsSuccessStatusCode)
        {
            // Read file content as byte array
            var fileBytes = await response.Content.ReadAsByteArrayAsync();

            // Convert the byte array to a stream
            var memoryStream = new MemoryStream(fileBytes);

            // Create a DotNetStreamReference from the memory stream
            var streamRef = new DotNetStreamReference(memoryStream);

            // Return the stream reference as part of the ApiResponse
            return new ApiResponse
            {
                Success = true,
                Data = streamRef  // Store the stream in the Data property
            };
        }
        else
        {
            // Log the error response for debugging
            var resultMsg = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(resultMsg, _jsonSerializerOptions);
            return new ApiResponse { Success = false, ErrorResponse = errorResponse };
        }
    }

    public async Task<bool> CheckAuthorizationAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{ApiRoutes.v1}/{ApiRoutes.UploadFiles.CheckAuthorization}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

}
