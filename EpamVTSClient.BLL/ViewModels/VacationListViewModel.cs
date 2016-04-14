using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using EpamVTSClient.BLL.Services;
using EpamVTSClient.BLL.ViewModels.Base;
using EpamVTSClient.Core.Services.Localization;
using EpamVTSClient.DAL.Models;
using Xamarin.Forms;

namespace EpamVTSClient.BLL.ViewModels
{
    public class VacationListViewModel : ViewModelBase
    {
        private readonly IVacationListService _vacationListService;
        private readonly IL10n _l10N;
        private ObservableCollection<VacationViewModel> _vacationViewModel;

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

        public VacationListViewModel(IVacationListService vacationListService, IL10n l10N)
        {
            _vacationListService = vacationListService;
            _l10N = l10N;
            LoadData = new Command(async () => await LoadDataAsync());
            LoadData.Execute(null);
        }

        public Command LoadData { get; set; }

        public async Task LoadDataAsync()
        {
            IEnumerable<ShortVacationInfo> result = await _vacationListService.GetVacationsAsync();
            IEnumerable<VacationViewModel> vacationViewModels = result.Select(x => new VacationViewModel(_l10N)
            {
                Type = _l10N.Localize(x.Type.ToString()),
                Id = x.Id,
                ApproverFullName = x.ApproverFullName,
                EndDate = x.EndDate,
                StartDate = x.StartDate,
                Status = x.Status,
                VacationStatusToDisplay = x.Status.ToString()
            });
            VacationList = new ObservableCollection<VacationViewModel>(vacationViewModels);
        }
    }
}
