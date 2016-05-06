using System;
using Cirrious.FluentLayouts.Touch;
using EpamVTSClient.BLL.ViewModels;
using EpamVTSClientNative.iOS.Helpers;
using UIKit;

namespace EpamVTSClientNative.iOS.Controllers
{
    public class LoginPageViewController : BaseViewController<LoginPageViewModel>
    {
        private UITextField _userNameTextField;
        private UITextField _passwordTextField;
        private UIImageView _logoImage;
        private UILabel _deviceInfoLabel;
        private UILabel _errorLabel;
        private UIButton _loginButton;
        private UIStackView _stack;

        protected override void Initialize()
        {
            base.Initialize();

            _logoImage = new UIImageView(UIImage.FromBundle("logo"));
            _userNameTextField = ControlsExtensions.SetTextField(LocalizationService.Localize("UserName"));
            _passwordTextField = ControlsExtensions.SetSecureTextField(LocalizationService.Localize("Password"));
            _deviceInfoLabel = ControlsExtensions.SetUiLabel(ViewModel.Copyright);
            _errorLabel = ControlsExtensions.SetErrorUiLabel();

            _loginButton = ControlsExtensions.SetButton(LocalizationService.Localize("LoginBtn"));
            _loginButton.TouchUpInside += LoginButtonOnTouchUpInside;

            _stack = new UIStackView { Axis = UILayoutConstraintAxis.Vertical };
            _stack.AddArrangedSubview(_logoImage);
            _stack.AddArrangedSubview(_userNameTextField);
            _stack.AddArrangedSubview(_passwordTextField);
            _stack.AddArrangedSubview(_errorLabel);
            _stack.AddArrangedSubview(_loginButton);
            _stack.AddArrangedSubview(_deviceInfoLabel);

            Add(_stack);

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            View.InsertSubview(new UIImageView(UIImage.FromBundle("illustration")), 0);

            AddConstraints();

            _userNameTextField.Text = "dz@epam.com";
            _passwordTextField.Text = "test1";

            ViewModel.PropertyChanged += (o, args) =>
            {
                if (args.PropertyName == "ErrorMessage")
                {
                    _errorLabel.Text = ViewModel.ErrorMessage;
                    _errorLabel.Enabled = true;
                }
            };
        }

        private void AddConstraints()
        {
            const int margin = 20;
            View.AddConstraints(
                _stack.CenterX().EqualTo().CenterXOf(View),
                _stack.CenterY().EqualTo().CenterYOf(View),

                _logoImage.AtTopOf(_stack),
                _logoImage.Width().EqualTo(150),
                _logoImage.CenterX().EqualTo().CenterXOf(View),

                _userNameTextField.Below(_logoImage, margin),
                _userNameTextField.Width().EqualTo(250),
                _userNameTextField.CenterX().EqualTo().CenterXOf(View),

                _passwordTextField.Below(_userNameTextField, margin),
                _passwordTextField.Width().EqualTo(250),
                _passwordTextField.CenterX().EqualTo().CenterXOf(View),

                _errorLabel.Below(_passwordTextField, margin),
                _errorLabel.Width().EqualTo().WidthOf(View),
                _errorLabel.AtLeftOf(View, margin),
                _errorLabel.CenterX().EqualTo().CenterXOf(View),

                _loginButton.Below(_errorLabel, margin),
                _loginButton.Width().EqualTo(100),
                _loginButton.CenterX().EqualTo().CenterXOf(View),

                _deviceInfoLabel.Below(_loginButton, 50),
                _deviceInfoLabel.CenterX().EqualTo().CenterXOf(View));
        }

        private void LoginButtonOnTouchUpInside(object sender, EventArgs eventArgs)
        {
            _errorLabel.Enabled = false;
            ViewModel.ErrorMessage = string.Empty;

            ViewModel.UserName = _userNameTextField.Text;
            ViewModel.Password = _passwordTextField.Text;

            ViewModel.SignIn.Execute(null);
        }
    }
}
