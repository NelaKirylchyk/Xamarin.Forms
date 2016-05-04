using EpamVTSClient.BLL.Services;
using EpamVTSClient.BLL.ViewModels.Base;
using EpamVTSClient.Core.Services.Localization;
using EpamVTSClientNative.iOS.Services;
using Microsoft.Practices.Unity;
using UIKit;

namespace EpamVTSClientNative.iOS.Controllers
{
    public class BaseViewController<TViewModel> : UIViewController where TViewModel : ViewModelBase
    {
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
