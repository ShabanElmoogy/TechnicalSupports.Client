namespace TechnicalSupport.Client.Core.Services.LocalizationService;

public interface ILocalizationService
{
    Task<Dictionary<string, string>> GetLocalizationData(string apiName, string language);

    Task<LocalizationResponse> UpdateLocalizationKey(string language, string key, string value);
}
