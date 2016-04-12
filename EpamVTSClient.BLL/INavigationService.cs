using System.Threading.Tasks;
using EpamVTSClient.BLL.ViewModels.Base;

namespace EpamVTSClient.BLL
{
    public interface INavigationService 
    {
        Task NavigateToAsync<TViewModelTo>() where TViewModelTo : ViewModelBase;
    }
}