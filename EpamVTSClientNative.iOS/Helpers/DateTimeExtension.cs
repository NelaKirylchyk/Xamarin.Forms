using System;
using Foundation;

namespace EpamVTSClientNative.iOS.Helpers
{
    public static class DateTimeExtension
    {
        public static NSDate ConvertDateTimeToNsDate(this DateTime date)
        {
            DateTime newDate = TimeZone.CurrentTimeZone.ToLocalTime(
                new DateTime(2001, 1, 1, 0, 0, 0));
            return NSDate.FromTimeIntervalSinceReferenceDate(
                (date - newDate).TotalSeconds);
        }

        public static DateTime NsDateToDateTime(this NSDate date)
        {
            DateTime reference = new DateTime(2001, 1, 1, 0, 0, 0);
            DateTime currentDate = reference.AddSeconds(date.SecondsSinceReferenceDate);
            DateTime localDate = currentDate.ToLocalTime();
            return localDate;
        }
    }
}
