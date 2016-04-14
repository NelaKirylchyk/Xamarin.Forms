﻿using System.Globalization;
using System.Reflection;
using System.Resources;

namespace EpamVTSClient.Core
{
    public class L10n : IL10n
    {
        private readonly ILocalize _localize;

        public L10n(ILocalize localize)
        {
            _localize = localize;
        }

        public void SetLocale()
        {
            _localize.SetLocale();
        }

        /// <remarks>
        /// Maybe we can cache this info rather than querying every time
        /// </remarks>
        public string Locale()
        {
            return _localize.GetCurrentCultureInfo().Name;
        }

        public string Localize(string key, string comment)
        {
            var netLanguage = Locale();
            // Platform-specific
            ResourceManager temp = new ResourceManager("XamarinEpamVTSClient.Resx.AppResources", typeof(L10n).GetTypeInfo().Assembly);
            string result = temp.GetString(key, new CultureInfo(netLanguage));
            return result;
        }
    }

    public interface IL10n
    {
        void SetLocale();
    }
}