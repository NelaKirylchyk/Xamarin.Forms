using System;
using System.Drawing;
using Cirrious.FluentLayouts.Touch;
using EpamVTSClient.BLL.ViewModels;
using EpamVTSClientNative.iOS.Services;
using SidebarNavigation;
using UIKit;

namespace EpamVTSClientNative.iOS.Controllers
{
    public class LoginPageViewController : BaseViewController<LoginPageViewModel>
    {
        public SidebarController SidebarController { get; private set; }

        private UITextField _userNameTextField;
        private UITextField _passwordTextField;
        protected override void Initialize()
        {
            base.Initialize();

            var logoImage = new UIImageView(UIImage.FromBundle("logo"));
            _userNameTextField = ControlsExtensions.SetTextField(LocalizationService.Localize("UserName"));
            _passwordTextField = ControlsExtensions.SetSecureTextField(LocalizationService.Localize("Password"));
            var deviceInfoLabel = ControlsExtensions.SetUiLabel(ViewModel.Copyright);
            var loginButton = ControlsExtensions.SetButton(LocalizationService.Localize("LoginBtn"));
            loginButton.TouchUpInside += LoginButtonOnTouchUpInside;

            var stack = new UIStackView { Axis = UILayoutConstraintAxis.Vertical };

            stack.AddArrangedSubview(logoImage);
            stack.AddArrangedSubview(_userNameTextField);
            stack.AddArrangedSubview(_passwordTextField);
            stack.AddArrangedSubview(loginButton);
            stack.AddArrangedSubview(deviceInfoLabel);

            Add(stack);
            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            View.InsertSubview(new UIImageView(UIImage.FromBundle("illustration")), 0);

            const int margin = 20;
            View.AddConstraints(
                stack.CenterX().EqualTo().CenterXOf(View),
                stack.CenterY().EqualTo().CenterYOf(View),
                _userNameTextField.Below(logoImage, margin),
                _passwordTextField.Below(_userNameTextField, margin),
                loginButton.Below(_passwordTextField, margin),
                deviceInfoLabel.Below(loginButton, 50));

            _userNameTextField.Text = "dz@epam.com";
            _passwordTextField.Text = "test1";
        }

        private void LoginButtonOnTouchUpInside(object sender, EventArgs eventArgs)
        {
            ViewModel.UserName = _userNameTextField.Text;
            ViewModel.Password = _passwordTextField.Text;
            ViewModel.SignIn.Execute(null);
        }
    }
}
