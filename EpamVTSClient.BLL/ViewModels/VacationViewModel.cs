using System;
using System.Threading.Tasks;
using System.Windows.Input;
using EpamVTSClient.BLL.Services;
using EpamVTSClient.BLL.ViewModels.Base;
using EpamVTSClient.Core.Enums;
using EpamVTSClient.Core.Services.Localization;
using Xamarin.Forms;

namespace EpamVTSClient.BLL.ViewModels
{
    public class VacationViewModel : ViewModelBase
    {
        private readonly ILocalizationService _localizationService;
        private readonly IVacationListService _vacationListService;
        private string _vacationStatusToDisplay;
        public VacationViewModel(ILocalizationService localizationService, INavigationService navigationService, IVacationListService vacationListService)
        {
            _localizationService = localizationService;
            _vacationListService = vacationListService;
            ViewDetails = new Command(() => navigationService.NavigateTo<VacationViewModel>(Id.ToString()));
        }

        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ApproverFullName { get; set; }

        public VacationStatus Status { get; set; }

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

        public async Task LoadDataFrom(int parse)
        {
            var vacationInfo = await _vacationListService.GetFullVacationInfoAsync(parse);
            Status = vacationInfo.Status;
            Type = vacationInfo.Type.ToString();
            StartDate = vacationInfo.StartDate;
            EndDate = vacationInfo.EndDate;
        }
    }
}
