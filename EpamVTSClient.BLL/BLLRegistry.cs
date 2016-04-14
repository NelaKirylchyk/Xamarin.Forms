using System.Net.Http;
using EpamVTSClient.BLL.Services;
using EpamVTSClient.BLL.ViewModels;
using EpamVTSClient.Core;
using Microsoft.Practices.Unity;

namespace EpamVTSClient.BLL
{
    public class BLLRegistry : IUnityContainerRegistry
    {
        public void Register(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<LoginPageViewModel>();
            unityContainer.RegisterType<VacationListViewModel>();
            unityContainer.RegisterType<VacationViewModel>();

            unityContainer.RegisterType<ILoginService, LogInService>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IVacationListService, VacationListService>();
            unityContainer.RegisterInstance(new HttpClient(new HttpClientHandler()));

            unityContainer.RegisterType<IL10n, L10n>();
        }
    }
}
