using EpamVTSClient.Core.Services;
using Microsoft.Practices.Unity;

namespace EpamVTSClientNative.Droid
{
    public class UIRegistry : IUnityContainerRegistry
    {
        public void Register(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<IPlatformSpecificInfoService, PlatformSpecificInfoService>(new ContainerControlledLifetimeManager());
            // unityContainer.RegisterType<INavigationService, NavigationService>();
            unityContainer.RegisterInstance(unityContainer);
            //unityContainer.RegisterInstance(Application.Current.MainPage.Navigation);
            unityContainer.RegisterType<IMessageDialogService, MessageDialogService>();
        }
    }
}