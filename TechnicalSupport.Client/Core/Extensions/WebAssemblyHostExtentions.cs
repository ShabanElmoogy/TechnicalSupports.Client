namespace TechnicalSupport.Client.Core.Extensions;

public static class WebAssemblyHostExtentions
{
    public async static Task SetDefaultCulture(this WebAssemblyHost host)
    {
        var _jsRuntime = host.Services.GetRequiredService<IJSRuntime>();

        var _result = await _jsRuntime.InvokeAsync<string>(JsFunctions.GetCookie, Strings.ClientCulture);
        var _browserLanguage = await _jsRuntime.InvokeAsync<string>(JsFunctions.GetBrowserLanguage);

        CultureInfo _culture;
        if (!string.IsNullOrEmpty(_result))
            _culture = new CultureInfo(_result);
        else
            _culture = new CultureInfo(_browserLanguage);

        CultureInfo.DefaultThreadCurrentCulture = _culture;
        CultureInfo.DefaultThreadCurrentUICulture = _culture;
    }
}