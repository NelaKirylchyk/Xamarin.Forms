using System;
using Android.App;
using Android.OS;
using EpamVTSClient.BLL.ViewModels;

namespace EpamVTSClientNative.Droid.Activities
{
    [Activity]
    public class VacationListActivity : ActivityBase<VacationListViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.VacationList);

        }
    }
}