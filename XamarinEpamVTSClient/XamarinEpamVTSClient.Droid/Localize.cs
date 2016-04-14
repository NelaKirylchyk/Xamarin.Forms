using System.Threading;
using EpamVTSClient.Core;

namespace XamarinEpamVTSClient.Droid
{
    public class Localize : ILocalize
    {
        public void SetLocale()
        {

            var androidLocale = Java.Util.Locale.Default; // user's preferred locale
            var netLocale = androidLocale.ToString().Replace("_", "-");
            var ci = new System.Globalization.CultureInfo(netLocale);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }
        public System.Globalization.CultureInfo GetCurrentCultureInfo()
        {
            var androidLocale = Java.Util.Locale.Default;
            var netLanguage = androidLocale.ToString().Replace("_", "-"); // turns pt_BR into pt-BR
            return new System.Globalization.CultureInfo(netLanguage);
        }
    }
}