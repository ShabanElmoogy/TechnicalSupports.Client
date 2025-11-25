namespace TechnicalSupport.Services.Models;

public class LocalizationResponse 
{
    public Dictionary<string, string>? Value { get; set; }
    public string? ErrorMessage { get; set; }
    public bool Success { get; set; } = true;
}