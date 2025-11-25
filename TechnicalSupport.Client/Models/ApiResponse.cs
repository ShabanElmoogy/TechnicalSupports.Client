namespace TechnicalSupport.Services.Models;

public class ApiResponse 
{
    public bool Success { get; set; }
    public object? Data { get; set; }
    public ErrorResponse? ErrorResponse { get; set; }
}

// Generic version of ApiResponse to hold specific entity types
public class ApiResponse<T> : ApiResponse
{
    public T? Entity { get; set; }

    public List<T>? Entities { get; set; }
}


