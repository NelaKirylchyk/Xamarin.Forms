using System.Globalization;
using System.Reflection;
using System.Resources;
using EpamVTSClient.Core.Services.Localization;

namespace EpamVTSClient.Core
{
    public class LocalizationService : ILocalizationService
    {
        private readonly ILocalize _localize;

        public LocalizationService(ILocalize localize)
        {
            _localize = localize;
        }

        public void SetLocale()
        {
            _localize.SetLocale();
        }
        
        public string Locale()
        {
            return _localize.GetCurrentCultureInfo().Name;
        }

        public string Localize(string key)
        {
            var netLanguage = Locale();

            ResourceManager temp = new ResourceManager("EpamVTSClient.Core.Resx.AppResources", typeof(LocalizationService).GetTypeInfo().Assembly);
            string result = temp.GetString(key, new CultureInfo(netLanguage));
            return result;
        }
    }
}
