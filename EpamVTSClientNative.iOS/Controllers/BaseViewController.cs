using EpamVTSClient.BLL.Services;
using EpamVTSClient.BLL.ViewModels.Base;
using EpamVTSClient.Core.Services.Localization;
using EpamVTSClientNative.iOS.Services;
using Foundation;
using Microsoft.Practices.Unity;
using SidebarNavigation;
using UIKit;

namespace EpamVTSClientNative.iOS.Controllers
{
    public class BaseViewController<TViewModel> : UIViewController where TViewModel : ViewModelBase
    {
        public SidebarController SidebarController
        {
            get
            {
                return (UIApplication.SharedApplication.Delegate as AppDelegate).SidebarController;
                //return (UIApplication.SharedApplication.Delegate as AppDelegate).RooSidebarController;
            }
        }
        
        protected ILocalizationService LocalizationService { get; set; }

        public string args { get; set; }
        protected INavigationService NavigationService { get; set; }
        public TViewModel ViewModel { get; set; }

        public BaseViewController()
        {
            LocalizationService = Factory.UnityContainer.Resolve<ILocalizationService>();
            ViewModel = Factory.UnityContainer.Resolve<TViewModel>();
            NavigationService = Factory.UnityContainer.Resolve<INavigationService>();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Initialize();
        }

        protected virtual void Initialize()
        {
            
        }
    }
}
