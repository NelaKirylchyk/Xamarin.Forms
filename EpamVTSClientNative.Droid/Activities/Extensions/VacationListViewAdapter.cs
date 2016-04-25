using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using EpamVTSClient.BLL.ViewModels;

namespace EpamVTSClientNative.Droid.Activities
{
    public class VacationListViewAdapter : BaseAdapter<VacationViewModel>
    {
        protected Activity Context = null;
        protected List<VacationViewModel> VacationItems;

        public VacationListViewAdapter(Activity context, List<VacationViewModel> services)
        {
            this.Context = context;
            this.VacationItems = services;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override int Count => VacationItems.Count;
        public override VacationViewModel this[int position] => VacationItems[position];
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? Context.LayoutInflater.Inflate(Resource.Layout.VacationListItem, null);

            var vacationViewModel = this[position];
            view.FindViewById<TextView>(Resource.Id.vacationTypeTextView).Text = vacationViewModel.Type;
            view.FindViewById<TextView>(Resource.Id.vacationStatusTextView).Text = vacationViewModel.VacationStatusToDisplay;
            view.FindViewById<TextView>(Resource.Id.dateTimeTextView).Text = $"{vacationViewModel.StartDate} - {vacationViewModel.EndDate}";

            return view;
        }
    }
}