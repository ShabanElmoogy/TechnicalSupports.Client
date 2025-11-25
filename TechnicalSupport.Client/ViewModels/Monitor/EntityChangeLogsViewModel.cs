namespace TechnicalSupport.Client.ViewModels.Monitor;

public class EntityChangeLogsViewModel
{
    public int ChangeLogId { get; set; }
    public string? EntityName { get; set; }
    public string? Key { get; set; }
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public string? ChangedBy { get; set; }
    public DateTime ChangedAt { get; set; }
    public string? ChangedByPc { get; set; }
}

