using Android.App;
using Android.OS;
using EpamVTSClient.BLL.ViewModels;

namespace EpamVTSClientNative.Droid.Activities
{
    [Activity(MainLauncher = true, Icon = "@drawable/icon")]
    public class LoginActivity : ActivityBase<LoginPageViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            this.BindCommand(Resource.Id.loginBtn, ViewModel.SignIn);
            this.BindText(Resource.Id.emailText, ViewModel, vm => vm.UserName);
            this.BindText(Resource.Id.passwordText, ViewModel, vm => vm.Password);

            ViewModel.UserName = "dz@epam.com";
            ViewModel.Password = "test1";
        }
    }
}

