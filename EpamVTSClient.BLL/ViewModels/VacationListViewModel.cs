using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using EpamVTSClient.BLL.Services;
using EpamVTSClient.BLL.ViewModels.Base;
using EpamVTSClient.DAL.Models;
using Xamarin.Forms;

namespace EpamVTSClient.BLL.ViewModels
{
    public class VacationListViewModel : ViewModelBase
    {
        private readonly IVacationListService _vacationListService;
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

        public VacationListViewModel(IVacationListService vacationListService)
        {
            _vacationListService = vacationListService;
            LoadData = new Command(async () => await LoadDataAsync());
            LoadData.Execute(null);
        }

        public Command LoadData { get; set; }

        public async Task LoadDataAsync()
        {
            IEnumerable<ShortVacationInfo> result = await _vacationListService.GetVacationsAsync();
            IEnumerable<VacationViewModel> vacationViewModels = result.Select(x => new VacationViewModel()
            {
                Type = x.Type,
                Id = x.Id,
                ApproverFullName = x.ApproverFullName,
                EndDate = x.EndDate,
                StartDate = x.StartDate,
                Status = x.Status
            });
            VacationList = new ObservableCollection<VacationViewModel>(vacationViewModels);
        }
    }
}
