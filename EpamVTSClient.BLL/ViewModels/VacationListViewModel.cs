using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EpamVTSClient.BLL.Services;
using EpamVTSClient.BLL.ViewModels.Base;
using EpamVTSClient.Core.Services;
using EpamVTSClient.Core.Services.Localization;
using EpamVTSClient.DAL.Models;
using Xamarin.Forms;

namespace EpamVTSClient.BLL.ViewModels
{
    public class VacationListViewModel : ViewModelBase
    {
        private readonly IVacationListService _vacationListService;
        private readonly ILocalizationService _localizationService;
        private ObservableCollection<VacationViewModel> _vacationViewModel;
        private readonly INavigationService _navigationService;
        private readonly ILoginService _loginService;
        private IMessageDialogService _messageDialogService;

        public ObservableCollection<VacationViewModel> VacationList
        {
            get { return _vacationViewModel; }
            set
            {
                if (_vacationViewModel != value)
                {
                    _vacationViewModel = value;
                    OnPropertyChanged();
                }
            }
        }

        public VacationListViewModel(IVacationListService vacationListService, ILocalizationService localizationService, INavigationService navigationService, ILoginService loginService, IMessageDialogService messageDialogService)
        {
            _vacationListService = vacationListService;
            _localizationService = localizationService;
            _navigationService = navigationService;
            _loginService = loginService;
            _messageDialogService = messageDialogService;
            LoadData = new Command(() => Task.Run(LoadDataAsync).Wait());
            LoadData.Execute(null);
        }

        public ICommand LoadData { get; set; }

        public async Task LoadDataAsync()
        {
            try
            {
                IEnumerable<ShortVacationInfo> result = await _vacationListService.GetVacationsAsync();
                IEnumerable<VacationViewModel> vacationViewModels = result.Select(x => new VacationViewModel(_localizationService, _navigationService, _vacationListService, _loginService, _messageDialogService)
                {
                    Type = _localizationService.Localize(x.Type.ToString()),
                    Id = x.Id,
                    ApproverFullName = x.ApproverFullName,
                    EndDate = x.EndDate,
                    StartDate = x.StartDate,
                    VacationStatus = x.Status,
                    VacationStatusToDisplay = x.Status.ToString()

                });
                VacationList = new ObservableCollection<VacationViewModel>(vacationViewModels);
            }
            catch (Exception e)
            {
                
            }
        }
    }
}
