namespace TechnicalSupport.Client.Pagination;

public class PaginationResponse<T> where T : class
{
    public List<T> Items { get; set; } = new();

    public MetaData? MetaData { get; set; }
}
