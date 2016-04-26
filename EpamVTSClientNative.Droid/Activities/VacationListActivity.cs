using System.Linq;
using Android.App;
using Android.OS;
using EpamVTSClient.BLL.ViewModels;
using EpamVTSClientNative.Droid.Activities.Base;
using EpamVTSClientNative.Droid.Activities.Extensions;

namespace EpamVTSClientNative.Droid.Activities
{
    [Activity]
    public class VacationListActivity : ActivityBase<VacationListViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.VacationList);
            this.BindListView(Resource.Id.VacationListView, ViewModel, ViewModel.VacationList.ToList());

            InitSideMenu(LocalizationService.Localize("VacationListTitle"));
        }
    }
}