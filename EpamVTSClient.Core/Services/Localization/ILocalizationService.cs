namespace EpamVTSClient.Core.Services.Localization
{
    public interface ILocalizationService
    {
        void SetLocale();
        string Localize(string key);
    }
}