namespace TechnicalSupport.Client.Core.StateManagement.StateContainer;

public class CultureService
{
    private string _currentCulture;

    // Event to notify culture change
    public event Action OnCultureChanged;

    // Method to get the current culture
    public string GetCurrentCulture() => _currentCulture;

    // Method to set the culture
    public void SetCurrentCulture(string culture)
    {
        _currentCulture = culture;
        OnCultureChanged?.Invoke();  // Notify components about the culture change
    }
}

