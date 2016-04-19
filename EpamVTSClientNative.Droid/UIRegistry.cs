using EpamVTSClient.BLL.Services;
using EpamVTSClient.Core.Services;
using EpamVTSClientNative.Droid.Services;
using Microsoft.Practices.Unity;

namespace EpamVTSClientNative.Droid
{
    public class UIRegistry : IUnityContainerRegistry
    {
        public void Register(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<IDeviceInfoService, DeviceInfoService>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<INavigationService, NavigationService>();
            unityContainer.RegisterInstance(unityContainer);
            //unityContainer.RegisterInstance(Application.Current.MainPage.Navigation);
            unityContainer.RegisterType<IMessageDialogService, MessageDialogService>();
        }
    }
}