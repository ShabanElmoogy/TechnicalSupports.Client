namespace TechnicalSupport.Client.Core.Services.FileService;

public interface IFileService
{
    Task<ApiResponse> UploadFileAsync(InputFileChangeEventArgs e, string uploadUrl, long maxFileSize = int.MaxValue);

    Task<ApiResponse> DownloadFileAsync(string downloadUrl, string storedFileName, string fileName);

    Task<ApiResponse> DownLoadStream(string downloadUrl, Guid id);
    Task<bool> CheckAuthorizationAsync();
}
