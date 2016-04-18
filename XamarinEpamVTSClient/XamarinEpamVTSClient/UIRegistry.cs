using EpamVTSClient.BLL.Services;
using EpamVTSClient.Core.Services;
using Microsoft.Practices.Unity;
using Xamarin.Forms;

namespace XamarinEpamVTSClient
{
    public class UIRegistry : IUnityContainerRegistry
    {
        public void Register(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<IPlatformSpecificInfoService, PlatformSpecificInfoService>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<INavigationService, NavigationService>();
            unityContainer.RegisterInstance(unityContainer);
            unityContainer.RegisterInstance(Application.Current.MainPage.Navigation);
            unityContainer.RegisterType<IMessageDialogService, MessageDialogService>();
        }
    }
}