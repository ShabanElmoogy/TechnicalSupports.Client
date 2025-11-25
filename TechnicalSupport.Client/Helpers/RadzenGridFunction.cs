namespace TechnicalSupport.Client.Helpers;

public class RadzenGridFunction
{
    public async Task GoToPageByAsync<T>(List<T> items, T currentItem, RadzenDataGrid<T> grid, Func<T, T, bool> comparer)
    {
        if (items == null || !items.Any() || grid == null)
            return;

        var currentIndex = items.FindIndex(item => comparer(item, currentItem));
        if (currentIndex >= 0 && grid.PageSize > 0)
        {
            var targetPage = Math.Max(0, currentIndex / grid.PageSize);
            await grid.GoToPage(targetPage);
        }
    }
}
