using System.Globalization;
using System.Threading;
using EpamVTSClient.Core.Services.Localization;
using Foundation;

namespace XamarinEpamVTSClient.iOS
{
    public class Localize : ILocalize
    {
        public void SetLocale()
        {
            var ci = new CultureInfo(GetCurrentCultureInfo().Name);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }
        public CultureInfo GetCurrentCultureInfo()
        {
            var netLanguage = "en";
            var prefLanguageOnly = "en";
            if (NSLocale.PreferredLanguages.Length > 0)
            {
                var preferredLanguage = NSLocale.PreferredLanguages[0];
                prefLanguageOnly = preferredLanguage.Substring(0, 2);
                if (prefLanguageOnly == "pt")
                {
                    preferredLanguage = preferredLanguage == "pt" ? "pt-BR" : "pt-PT";
                }
                netLanguage = preferredLanguage.Replace("_", "-");
            }
            CultureInfo cultureInfo;
            try
            {
                cultureInfo = new CultureInfo(netLanguage);
            }
            catch
            {
                // iOS locale not valid .NET culture (eg. "en-ES" : English in Spain)
                // fallback to first characters, in this case "en"
                cultureInfo = new CultureInfo(prefLanguageOnly);
            }
            return cultureInfo;
        }
    }
}