using System;
using Android.Widget;

namespace EpamVTSClientNative.Droid.Helpers
{
    public class DateChangedListener : Java.Lang.Object, DatePicker.IOnDateChangedListener
    {
        readonly Action<DatePicker, int, int, int> _callback;

        public DateChangedListener(Action<DatePicker, int, int, int> callback)
        {
            _callback = callback;
        }

        public void OnDateChanged(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            _callback(view, year, monthOfYear, dayOfMonth);
        }
    }
}