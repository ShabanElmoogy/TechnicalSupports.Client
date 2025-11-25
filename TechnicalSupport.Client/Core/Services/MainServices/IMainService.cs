namespace TechnicalSupport.Client.Core.Services.MainServices;

public interface IMainService<T> where T : class
{
    Task<List<T>> GetAll(string ApiName);
    Task<PaginationResponse<T>> GetAllWithPagination(PaginationParameters paginationParameters, string ApiName);
    Task<T> GetRow(string ApiName);
    Task<ApiResponse<bool>> AddNewRow(T entity, string ApiName);
    Task<ApiResponse<bool>> AddNewRows(List<T> entities, string ApiName);
    Task<ApiResponse<T>> UpdateRow(T entity, string ApiName);
    Task<ApiResponse<T>> UpdateListRow(List<T> appointments, string ApiName);
    Task<ApiResponse<T>> DeleteRow(string ApiName);
}
