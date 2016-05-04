using EpamVTSClient.BLL.Services;
using EpamVTSClient.Core.Services;
using EpamVTSClientNative.iOS.Controllers;
using Microsoft.Practices.Unity;

namespace EpamVTSClientNative.iOS.Services
{
    public class UIRegistry : IUnityContainerRegistry
    {
        public void Register(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<IDeviceInfoService, DeviceInfoService>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<INavigationService, NavigationService>();
            unityContainer.RegisterInstance(unityContainer);
            unityContainer.RegisterType<IMessageDialogService, MessageDialogService>();

            unityContainer.RegisterType<LoginPageViewController>();
            unityContainer.RegisterType<VacationListViewController>();
            unityContainer.RegisterType<VacationViewController>();
        }
    }
}