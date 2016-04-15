using System;
using EpamVTSClient.Core.Enums;
using EpamVTSClient.Core.Services.Localization;

namespace EpamVTSClient.BLL.ViewModels
{
    public class VacationViewModel
    {
        private readonly ILocalizationService _localizationService;
        private string _vacationStatusToDisplay;
        public VacationViewModel(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
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
    }
}
