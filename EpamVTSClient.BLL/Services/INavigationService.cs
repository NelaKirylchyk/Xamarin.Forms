using System.Threading.Tasks;
using EpamVTSClient.BLL.ViewModels.Base;

namespace EpamVTSClient.BLL.Services
{
    public interface INavigationService
    {
        void NavigateTo<TViewModelTo>(string args) where TViewModelTo : ViewModelBase;

        Task NavigateToAsync<TViewModelTo>() where TViewModelTo : ViewModelBase;
    }
}