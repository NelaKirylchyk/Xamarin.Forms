using System.Net.Http;
using EpamVTSClient.BLL.Services;
using EpamVTSClient.BLL.ViewModels;
using EpamVTSClient.Core;
using EpamVTSClient.Core.Services;
using EpamVTSClient.Core.Services.Localization;
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
            unityContainer.RegisterType<EditVacationViewModel>();

            unityContainer.RegisterType<ILoginService, LogInService>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IVacationsService, VacationsService>();
            unityContainer.RegisterInstance(new HttpClient(new HttpClientHandler()));

            unityContainer.RegisterType<ILocalizationService, LocalizationService>(new ContainerControlledLifetimeManager());
        }
    }
}
