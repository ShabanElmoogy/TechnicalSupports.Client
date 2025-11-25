namespace TechnicalSupport.Client.ViewModels.UploadFiles;

public sealed class UploadFilesViewModel : AuditableEntity
{
    public string Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string StoredFileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
}
