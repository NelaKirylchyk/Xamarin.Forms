using System;
using System.Collections.Generic;
using System.Linq;

namespace EpamVTSClient.BLL.Helpers
{
    public static class EnumExtensions
    {
        public static TEnum? TryParseIgnoringCase<TEnum>(string value) where TEnum : struct
        {
            TEnum result;
            if (Enum.TryParse(value, true, out result))
            {
                return result;
            }
            return null;
        }

        public static TEnum ParseIgnoringCase<TEnum>(string value) where TEnum : struct
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value, true);
        }

        public static IEnumerable<TEnum> GetValues<TEnum>()
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
        }
    }
}