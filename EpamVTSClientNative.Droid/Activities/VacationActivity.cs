using Android.App;
using Android.OS;
using EpamVTSClient.BLL.ViewModels;

namespace EpamVTSClientNative.Droid.Activities
{
    [Activity]
    public class VacationActivity : ActivityBase<VacationViewModel>
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            string text = Intent.GetStringExtra("args");
            SetContentView(Resource.Layout.VacationInfo);

            await ViewModel.LoadDataFrom(int.Parse(text));
            //Task.Run(() => ViewModel.LoadDataFrom(int.Parse(text))).Wait();

            InitSideMenu();
        }
    }
}