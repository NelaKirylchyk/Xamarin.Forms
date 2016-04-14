namespace EpamVTSClient.Core.Services.Localization
{
    public interface IL10n
    {
        void SetLocale();
        string Localize(string key);
    }
}