using System.Linq;
using Android.App;
using Android.OS;
using Android.Widget;
using EpamVTSClient.BLL.ViewModels;

namespace EpamVTSClientNative.Droid.Activities
{
    [Activity]
    public class VacationListActivity : ActivityBase<VacationListViewModel>
    {
        private ListView _vacationListViewControl;
        private VacationListViewAdapter _vacationListViewAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.VacationList);

            _vacationListViewControl = FindViewById<ListView>(Resource.Id.VacationListView);
            _vacationListViewAdapter = new VacationListViewAdapter(this, ViewModel.VacationList.ToList());
            _vacationListViewControl.Adapter = _vacationListViewAdapter;

        }
    }
}