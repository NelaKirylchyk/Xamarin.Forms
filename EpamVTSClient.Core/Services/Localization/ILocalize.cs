using System.Globalization;

namespace EpamVTSClient.Core.Services.Localization
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();
        void SetLocale();
    }
}
