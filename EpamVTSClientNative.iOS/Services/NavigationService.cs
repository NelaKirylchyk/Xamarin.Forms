using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EpamVTSClient.BLL.Services;
using EpamVTSClient.BLL.ViewModels;
using EpamVTSClient.BLL.ViewModels.Base;
using EpamVTSClientNative.iOS.Controllers;
using EpamVTSClientNative.iOS.Helpers;
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

        public static readonly Dictionary<Type, Type> ViewModelPageContainer =
            new Dictionary<Type, Type>()
        {
                {typeof(VacationListViewModel), typeof(VacationListViewController)},
                {typeof(EditVacationViewModel), typeof(VacationViewController)}
        };

        private readonly UIWindow _window;

        public Task NavigateToAsync<TViewModelTo>(string args) where TViewModelTo : ViewModelBase
        {
            Type viewModelType = typeof(TViewModelTo);
            if (ViewModelPageContainer.ContainsKey(viewModelType))
            {
                Type controllerType = ViewModelPageContainer[viewModelType];
                var uiViewController = (UIViewController)Factory.UnityContainer.Resolve(controllerType);
                var baseViewController = uiViewController as BaseViewController<TViewModelTo>;

                UIViewController vc = _window.RootViewController;
                while (vc.PresentedViewController != null)
                {
                    vc = vc.PresentedViewController;
                }

                if (args != null && baseViewController != null)
                {
                    baseViewController.Args = args;
                }
                try
                {
                    if (baseViewController != null)
                    {
                        baseViewController.SidebarController.Disabled = false;
                        baseViewController.SidebarController.ChangeContentView(baseViewController);
                    }
                }
                catch (Exception e)
                {
                    // ignored
                }
            }
            return Task.FromResult(true);
        }
    }
}
