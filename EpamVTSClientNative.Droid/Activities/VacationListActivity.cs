using System.Linq;
using Android.App;
using Android.OS;
using Android.Widget;
using EpamVTSClient.BLL.ViewModels;
using EpamVTSClientNative.Droid.Activities.Base;
using EpamVTSClientNative.Droid.Activities.Extensions;

namespace EpamVTSClientNative.Droid.Activities
{
    [Activity]
    public class VacationListActivity : ActivityBase<VacationListViewModel>
    {
        private ListView _listView;
        private VacationListViewAdapter _listViewAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.VacationList);

            _listView = FindViewById<ListView>(Resource.Id.VacationListView);
            _listViewAdapter = new VacationListViewAdapter(this, ViewModel.VacationList.ToList());
            _listView.Adapter = _listViewAdapter;
            _listView.ItemClick += OnListItemClick;

            InitSideMenu(LocalizationService.Localize("VacationListTitle"));
        }

        void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            VacationViewModel item = _listViewAdapter[e.Position];
            item.ViewDetails.Execute(null);
        }
    }
}