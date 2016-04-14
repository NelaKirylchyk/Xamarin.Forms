using System;
using System.Threading.Tasks;
using EpamVTSClient.BLL.Services;
using EpamVTSClient.BLL.ViewModels.Base;
using EpamVTSClient.Core.Services.Localization;
using Xamarin.Forms;

namespace EpamVTSClient.BLL.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly ILoginService _loginService;
        private readonly IL10n _localization;
        private readonly INavigationService _navigationService;

        private string _userName;
        private string _password;
        private string _errorMessage;
        private bool _isBusy;

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged("UserName");
                }
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    OnPropertyChanged("ErrorMessage");
                }
            }
        }

        public string Copyright => $"{Device.OS} {DateTime.Now.ToString("d")}";

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged();
                    SignIn.ChangeCanExecute();
                }
                _isBusy = value;
            }
        }

        public Command SignIn { get; }

        public LoginPageViewModel(INavigationService navigation, ILoginService loginService, IL10n localization)
        {
            _loginService = loginService;
            _localization = localization;
            _navigationService = navigation;

            SignIn = new Command(async () => { await LoginAsync(); }, () => !IsBusy);
        }

        private async Task LoginAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
                {
                    ErrorMessage = _localization.Localize("IncorrectUserNameOrPasswordErrorMsg");
                    return;
                }
                IsBusy = true;
                var isLoggedIn = await _loginService.LogInAsync(UserName, Password);
                if (isLoggedIn)
                {
                    await _navigationService.NavigateToAsync<VacationListViewModel>();
                }
                ErrorMessage = "User name or password is incorrect.";
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
