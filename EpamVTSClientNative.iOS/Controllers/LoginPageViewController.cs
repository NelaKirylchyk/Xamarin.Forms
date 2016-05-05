using System;
using Cirrious.FluentLayouts.Touch;
using EpamVTSClient.BLL.ViewModels;
using EpamVTSClientNative.iOS.Helpers;
using SidebarNavigation;
using UIKit;

namespace EpamVTSClientNative.iOS.Controllers
{
    public class LoginPageViewController : BaseViewController<LoginPageViewModel>
    {
        private UITextField _userNameTextField;
        private UITextField _passwordTextField;
        private UIImageView _logoImage;
        private UILabel _deviceInfoLabel;
        private UIButton _loginButton;
        private UIStackView _stack;

        protected override void Initialize()
        {
            base.Initialize();

            _logoImage = new UIImageView(UIImage.FromBundle("logo"));
            _userNameTextField = ControlsExtensions.SetTextField(LocalizationService.Localize("UserName"));
            _passwordTextField = ControlsExtensions.SetSecureTextField(LocalizationService.Localize("Password"));
            _deviceInfoLabel = ControlsExtensions.SetUiLabel(ViewModel.Copyright);
            _loginButton = ControlsExtensions.SetButton(LocalizationService.Localize("LoginBtn"));
            _loginButton.TouchUpInside += LoginButtonOnTouchUpInside;

            _stack = new UIStackView { Axis = UILayoutConstraintAxis.Vertical };
            _stack.AddArrangedSubview(_logoImage);
            _stack.AddArrangedSubview(_userNameTextField);
            _stack.AddArrangedSubview(_passwordTextField);
            _stack.AddArrangedSubview(_loginButton);
            _stack.AddArrangedSubview(_deviceInfoLabel);

            Add(_stack);

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            View.InsertSubview(new UIImageView(UIImage.FromBundle("illustration")), 0);

            AddConstraints();

            _userNameTextField.Text = "dz@epam.com";
            _passwordTextField.Text = "test1";

            if (SidebarController != null)
            {
                SidebarController.Disabled = true;
            }
        }

        protected void AddConstraints()
        {
            const int margin = 20;
            View.AddConstraints(
                _stack.CenterX().EqualTo().CenterXOf(View),
                _stack.CenterY().EqualTo().CenterYOf(View),
                _userNameTextField.Below(_logoImage, margin),
                _passwordTextField.Below(_userNameTextField, margin),
                _loginButton.Below(_passwordTextField, margin),
                _deviceInfoLabel.Below(_loginButton, 50));
        }

        private void LoginButtonOnTouchUpInside(object sender, EventArgs eventArgs)
        {
            ViewModel.UserName = _userNameTextField.Text;
            ViewModel.Password = _passwordTextField.Text;
            ViewModel.SignIn.Execute(null);
        }
    }
}
