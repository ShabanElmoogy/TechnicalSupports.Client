using Microsoft.AspNetCore.WebUtilities;

namespace TechnicalSupport.Client.Core.Services.MainServices;

public class MainService<T> : IMainService<T> where T : class
{
    private readonly HttpClient _httpClient;
    private readonly HttpClient _reportClient;
    public NavigationManager _navigationManager;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public MainService(
        IHttpClientFactory httpClientFactory,
        NavigationManager navigationManager,
        HttpClient reportClient)
    {
        _httpClient = httpClientFactory.CreateClient(Strings.ApiClient);
        _navigationManager = navigationManager;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            ReferenceHandler = ReferenceHandler.Preserve,
            MaxDepth = 10
        };
        _reportClient = reportClient;
    }

    public async Task<List<T>> GetAll(string apiName)
    {
        var response = await _httpClient.GetAsync(apiName);

        if (!response.IsSuccessStatusCode)
        {
            HandleErrorResponse(response);
        }
        else
        {
            return await response.Content.ReadFromJsonAsync<List<T>>();
        }

        return [];
    }

    public async Task<PaginationResponse<T>> GetAllWithPagination(PaginationParameters paginationParameters, string apiName)
    {
        var queryParams = new Dictionary<string, string>()
        {
            [Strings.PageNumber] = paginationParameters.PageNumber.ToString(),
            [Strings.PageSize] = paginationParameters.PageSize.ToString(),
        };

        var response = await _httpClient.GetAsync(QueryHelpers.AddQueryString(apiName, queryParams));
        if (response.IsSuccessStatusCode)
        {
            // Check if the X-Pagination header is present
            if (response.Headers.TryGetValues(Strings.PaginationHeader, out var values))
            {
                var paginationHeader = values.FirstOrDefault();
                // Deserialize and use paginationHeader as needed
            }
        }
        var content = await response.Content.ReadAsStringAsync();


        if (!response.IsSuccessStatusCode)
        {
            HandleErrorResponse(response);
        }

        var paginationResponse = new PaginationResponse<T>
        {
            Items = JsonSerializer.Deserialize<List<T>>(content, _jsonSerializerOptions),
            MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues(Strings.PaginationHeader).FirstOrDefault(), _jsonSerializerOptions)
        };

        return paginationResponse;
    }

    public async Task<T> GetRow(string ApiName)
    {
        var respons = await _httpClient.GetAsync(ApiName);

        if (!respons.IsSuccessStatusCode)
        {
            HandleErrorResponse(respons);

            var Msg = await respons.Content.ReadAsStringAsync();
            throw new Exception(respons.ReasonPhrase + "_" + Msg);
        }
        else
        {
            var content = await respons.Content.ReadAsStringAsync();
            //Newtonsoft best for complex json
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);

            return result;
        }
    }
    public async Task<ApiResponse<bool>> AddNewRow(T entity, string ApiName)
    {
        var request = await _httpClient.PostAsJsonAsync(ApiName, entity);

        if (request.StatusCode == HttpStatusCode.Unauthorized)
        {
            HandleErrorResponse(request);
        }
        else if (!request.IsSuccessStatusCode)
        {
            var resultMsg = await request.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(resultMsg, _jsonSerializerOptions);
            return new ApiResponse<bool> { Success = false, ErrorResponse = errorResponse };
        }

        return new ApiResponse<bool> { Success = true };
    }

    public async Task<ApiResponse<bool>> AddNewRows(List<T> entities, string ApiName)
    {
        // Check if the entities list is null or empty
        if (entities == null || entities.Count == 0)
        {
            return new ApiResponse<bool>
            {
                Success = false,
                ErrorResponse = new ErrorResponse
                {
                    Errors = new Dictionary<string, List<string>>
                    {
                        { "Validation", new List<string> { "Entities list is empty." } }
                    }
                }
            };
        }
        // Send the POST request to the specified API with the entities list
        var request = await _httpClient.PostAsJsonAsync(ApiName, entities);

        // If the status code is not successful, handle the error response
        if (!request.IsSuccessStatusCode)
        {
            var resultMsg = await request.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(resultMsg, _jsonSerializerOptions);
            return new ApiResponse<bool> { Success = false, ErrorResponse = errorResponse };
        }

        // If successful, return success response with true
        return new ApiResponse<bool> { Success = true };
    }


    public async Task<ApiResponse<T>> UpdateRow(T entity, string ApiName)
    {
        var request = await _httpClient.PutAsJsonAsync(ApiName, entity);

        if (request.StatusCode.Equals(401))
        {
            HandleErrorResponse(request);
        }
        else if (!request.IsSuccessStatusCode)
        {
            var resultMsg = await request.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(resultMsg, _jsonSerializerOptions);
            return new ApiResponse<T> { Success = false, ErrorResponse = errorResponse };
        }
        return new ApiResponse<T> { Success = true, Entity = entity };
    }

    public async Task<ApiResponse<T>> UpdateListRow(List<T> appointments, string ApiName)
    {
        var request = await _httpClient.PutAsJsonAsync(ApiName, appointments);

        if (request.StatusCode.Equals(401))
        {
            HandleErrorResponse(request);
        }
        else if (!request.IsSuccessStatusCode)
        {
            var resultMsg = await request.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(resultMsg, _jsonSerializerOptions);
            return new ApiResponse<T> { Success = false, ErrorResponse = errorResponse };
        }

        // If you want to return a list as part of the response, adjust accordingly.
        return new ApiResponse<T> { Success = true, Entity = appointments[1] };
    }


    public async Task<ApiResponse<T>> DeleteRow(string ApiName)
    {
        var result = await _httpClient.DeleteAsync(ApiName);

        if (result.StatusCode.Equals(401))
        {
            HandleErrorResponse(result);
        }
        else if (!result.IsSuccessStatusCode)
        {
            var resultMsg = await result.Content.ReadAsStringAsync();
            var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(resultMsg, _jsonSerializerOptions);
            return new ApiResponse<T> { Success = false, ErrorResponse = errorResponse };
        }

        return new ApiResponse<T> { Success = true };
    }

    private void HandleErrorResponse(HttpResponseMessage response)
    {
        _navigationManager.NavigateTo($"{InternalRoutes.ErrorPage}/{response.StatusCode}");
    }
}
