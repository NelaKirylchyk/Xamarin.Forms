using Android.App;
using Android.OS;
using EpamVTSClient.BLL.ViewModels;
using EpamVTSClientNative.Droid.Activities.Base;
using EpamVTSClientNative.Droid.Activities.Extensions;

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
            this.BindText(Resource.Id.errorMessageTextView, ViewModel, vm => vm.ErrorMessage);
            this.BindText(Resource.Id.copyright, ViewModel, vm => vm.Copyright);

            this.BindLabel(Resource.Id.loginBtn, LocalizationService.Localize("LoginBtn"));
            this.BindHint(Resource.Id.emailText, LocalizationService.Localize("UserName"));
            this.BindHint(Resource.Id.passwordText, LocalizationService.Localize("Password"));

            ViewModel.UserName = "dz@epam.com";
            ViewModel.Password = "test1";
        }
    }
}

