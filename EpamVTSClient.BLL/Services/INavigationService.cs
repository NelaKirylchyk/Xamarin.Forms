using System.Threading.Tasks;
using EpamVTSClient.BLL.ViewModels.Base;

namespace EpamVTSClient.BLL.Services
{
    public interface INavigationService
    {
        Task NavigateToAsync<TViewModelTo>(string args) where TViewModelTo : ViewModelBase;
    }
}