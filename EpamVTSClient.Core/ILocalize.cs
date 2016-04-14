using System.Globalization;

namespace EpamVTSClient.Core
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();
        void SetLocale();
    }
}
