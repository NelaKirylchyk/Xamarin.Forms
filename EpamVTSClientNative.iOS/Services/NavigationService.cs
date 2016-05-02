using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EpamVTSClient.BLL.Services;
using EpamVTSClient.BLL.ViewModels;
using EpamVTSClient.BLL.ViewModels.Base;
using EpamVTSClientNative.iOS.Controllers;
using Microsoft.Practices.Unity;
using UIKit;

namespace EpamVTSClientNative.iOS.Services
{
    public class NavigationService : INavigationService
    {
        public NavigationService()
        {
            _window = WindowHelper.Window;
        }

        public static readonly Dictionary<Type, UIViewController> ViewModelPageContainer =
            new Dictionary<Type, UIViewController>()
        {
                {typeof(VacationListViewModel), Factory.UnityContainer.Resolve<VacationListViewController>()}
        };
            
        private readonly UIWindow _window;

        public Task NavigateToAsync<TViewModelTo>(string args) where TViewModelTo : ViewModelBase
        {
            Type type = typeof(TViewModelTo);
            if (ViewModelPageContainer.ContainsKey(type))
            {
                var uiViewController = ViewModelPageContainer[type];
                var baseViewController = uiViewController as BaseViewController<TViewModelTo>;

                UIViewController vc = _window.RootViewController;
                while (vc.PresentedViewController != null)
                {
                    vc = vc.PresentedViewController;
                }
                try
                {
                    vc.PresentViewController(baseViewController, false, null);
                }
                catch (Exception e)
                {
                    
                }
            }
            return Task.FromResult(true);
        }
    }
}
