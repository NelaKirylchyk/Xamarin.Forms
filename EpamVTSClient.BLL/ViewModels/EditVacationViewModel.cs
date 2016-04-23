using System.Threading.Tasks;
using System.Windows.Input;
using EpamVTSClient.BLL.Services;
using EpamVTSClient.Core.Enums;
using EpamVTSClient.Core.Helpers;
using EpamVTSClient.Core.Services.Localization;
using EpamVTSClient.DAL.Models;
using Xamarin.Forms;

namespace EpamVTSClient.BLL.ViewModels
{
    public class EditVacationViewModel : VacationViewModel
    {
        private readonly INavigationService _navigationService;
        public ICommand EditVacationCommand { get; set; }

        public ICommand CancelEditVacationCommand { get; set; }

        public EditVacationViewModel(ILocalizationService localizationService, INavigationService navigationService, IVacationListService vacationListService, ILoginService loginService) : base(localizationService, navigationService, vacationListService, loginService)
        {
            _navigationService = navigationService;
            EditVacationCommand = new Command(async () => { await EditAsync(); });
            CancelEditVacationCommand = new Command(() => { _navigationService.NavigateToAsync<VacationViewModel>(Id.ToString()); });
        }

        private async Task EditAsync()
        {
            await VacationListService.AddUpdateVacationInfoAsync(new VacationInfo()
            {
                StartDate = StartDate,
                EndDate = EndDate,
                Status = VacationStatus,
                Id = Id,
                Type = VacationType,
                VacationForm = VacationForm,
                EmployeeId = EmployeeId,
                ApproverId = ApproverId
            });
            await _navigationService.NavigateToAsync<VacationListViewModel>(null);
        }

        public string VacationForm { get; set; }

    }
}