using Android.App;
using Android.OS;
using EpamVTSClient.BLL.ViewModels.Base;
using Microsoft.Practices.Unity;

namespace EpamVTSClientNative.Droid.Activities
{
    [Activity(Label = "ActivityBase")]
    public class ActivityBase<TViewModel> : Activity where TViewModel: ViewModelBase
    {
        public ActivityBase()
        {
           
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ViewModel = Factory.UnityContainer.Resolve<TViewModel>();
        }

        public TViewModel ViewModel { get; private set; }
    }
}