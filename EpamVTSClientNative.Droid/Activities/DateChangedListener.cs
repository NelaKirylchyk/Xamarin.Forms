using System;
using Android.Widget;

namespace EpamVTSClientNative.Droid.Activities
{
    public class DateChangedListener : Java.Lang.Object, DatePicker.IOnDateChangedListener
    {

        Action<DatePicker, int, int, int> callback;

        public DateChangedListener(Action<DatePicker, int, int, int> callback)
        {
            this.callback = callback;
        }

        public void OnDateChanged(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        {
            callback(view, year, monthOfYear, dayOfMonth);
        }
    }
}