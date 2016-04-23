using System;
using System.Threading.Tasks;
using System.Windows.Input;
using EpamVTSClient.BLL.Services;
using EpamVTSClient.BLL.ViewModels.Base;
using EpamVTSClient.Core.Enums;
using EpamVTSClient.Core.Services.Localization;
using EpamVTSClient.DAL.Models;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace EpamVTSClient.BLL.ViewModels
{
    public class VacationViewModel : ViewModelBase
    {
        private readonly ILocalizationService _localizationService;
        private readonly INavigationService _navigationService;
        protected readonly IVacationListService VacationListService;
        private readonly ILoginService _loginService;
        private string _vacationStatusToDisplay;
        public VacationViewModel(ILocalizationService localizationService, INavigationService navigationService, IVacationListService vacationListService, ILoginService loginService)
        {
            _localizationService = localizationService;
            _navigationService = navigationService;
            VacationListService = vacationListService;
            _loginService = loginService;
            ViewDetails = new Command(() => navigationService.NavigateToAsync<VacationViewModel>(Id.ToString()));
            DeleteVacationCommand = new Command(async () => { await DeleteAsync(); });
            NavigateToEditViewCommand = new Command(() => navigationService.NavigateToAsync<EditVacationViewModel>(Id.ToString()));
        }

        private async Task DeleteAsync()
        {
            var isRemoved = await VacationListService.DeleteVacationAsync(Id);
            if(isRemoved)
                await _navigationService.NavigateToAsync<VacationListViewModel>(null);
            //else message dialog
        }

        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ApproverFullName { get; set; }

        public int ApproverId { get; set; }

        public VacationStatus VacationStatus { get; set; }

        public VacationType VacationType { get; set; }

        public string VacationStatusToDisplay
        {
            get
            {
                return _localizationService.Localize(_vacationStatusToDisplay);
            }
            set
            {
                if (_vacationStatusToDisplay != value)
                {
                    _vacationStatusToDisplay = value;
                }
            }
        }

        public string Type { get; set; }

        public ICommand ViewDetails { get; set; }

        public ICommand DeleteVacationCommand { get; set; }
        public ICommand NavigateToEditViewCommand { get; set; }

        public async Task LoadDataFrom(int vacationId)
        {
            var vacationInfo = await VacationListService.GetFullVacationInfoAsync(vacationId);

            Id = vacationInfo.Id;
            VacationStatus = vacationInfo.Status;
            VacationType = vacationInfo.Type;
            Type = _localizationService.Localize(vacationInfo.Type.ToString());
            StartDate = vacationInfo.StartDate;
            EndDate = vacationInfo.EndDate;
            VacationStatusToDisplay = vacationInfo.Status.ToString();
            EmployeeId = vacationInfo.EmployeeId;
            ApproverId = vacationInfo.ApproverId;
        }

        public void SetDefaultData()
        {
            VacationStatus = VacationStatus.None;
            VacationType = VacationType.None;
            Type = _localizationService.Localize(VacationType.ToString());
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            EmployeeId = _loginService.User.Id;
            ApproverId = EmployeeId;
        }
    }
}
