// Update the interface
public interface IDashboardNavigationService
{
    Task NavigateToPageOfEditedCompanyAsync(RadzenDataGrid<CompanyViewModel> dataGrid,
        List<CompanyViewModel> companies, int editedCompanyId);
    Task NavigateToLastPageAsync(RadzenDataGrid<CompanyViewModel> dataGrid);
    Task NavigateToLastPageAsync(RadzenDataGrid<CompanyViewModel> dataGrid, List<CompanyViewModel> companies);
}

// Update the service
public class DashboardNavigationService : IDashboardNavigationService
{
    public async Task NavigateToPageOfEditedCompanyAsync(RadzenDataGrid<CompanyViewModel> dataGrid,
        List<CompanyViewModel> companies, int editedCompanyId)
    {
        if (dataGrid == null || companies == null || !companies.Any())
            return;
        try
        {
            var companyIndex = companies.FindIndex(c => c.Id == editedCompanyId);
            if (companyIndex >= 0)
            {
                var pageSize = dataGrid.PageSize;
                var pageIndex = companyIndex / pageSize;
                await dataGrid.GoToPage(pageIndex);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error navigating to company page: {ex.Message}");
        }
    }

    public async Task NavigateToLastPageAsync(RadzenDataGrid<CompanyViewModel> dataGrid)
    {
        if (dataGrid == null) return;

        try
        {
            await dataGrid.Reload();

            var pageSize = dataGrid.PageSize;
            var count = dataGrid.Count;

            if (count > 0)
            {
                var lastPage = (int)Math.Ceiling((double)count / pageSize);
                await dataGrid.GoToPage(lastPage - 1);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error navigating to last page: {ex.Message}");
        }
    }

    // New overload that uses the actual data count
    public async Task NavigateToLastPageAsync(RadzenDataGrid<CompanyViewModel> dataGrid, List<CompanyViewModel> companies)
    {
        if (dataGrid == null || companies == null) return;

        try
        {
            var pageSize = dataGrid.PageSize;
            var count = companies.Count; // Use actual data count

            if (count > 0)
            {
                var lastPage = (int)Math.Ceiling((double)count / pageSize);
                await dataGrid.GoToPage(lastPage - 1);

                Console.WriteLine($"Navigated to last page {lastPage} (0-indexed: {lastPage - 1}) with {count} companies");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error navigating to last page: {ex.Message}");
        }
    }
}