namespace TechnicalSupport.Client.Core.Extensions;

public static class ClipboardExtensions
{
    // Extension method for IJSRuntime to copy text to clipboard
    public static async Task CopyTextToClipboard(this IJSRuntime jsRuntime, string text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            await jsRuntime.InvokeVoidAsync(JsFunctions.CopyToClipboard, text);
        }
    }

    // Extension method for IJSRuntime to get the text content of the body (CardBody)
    public static async Task<string> GetCardBodyText(this IJSRuntime jsRuntime, ElementReference elementReference)
    {
        if (elementReference.Context != null)
        {
            return await jsRuntime.InvokeAsync<string>(JsFunctions.GetCardBodyText, elementReference);
        }
        return string.Empty;
    }
}

