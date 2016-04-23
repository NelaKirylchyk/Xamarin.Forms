using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EpamVTSClient.BLL.Services;
using EpamVTSClient.BLL.ViewModels;
using EpamVTSClient.BLL.ViewModels.Base;
using Microsoft.Practices.Unity;
using Xamarin.Forms;
using XamarinEpamVTSClient.Views;

namespace XamarinEpamVTSClient
{
    public class NavigationService : INavigationService
    {
        private readonly INavigation _navigation;
        private readonly IUnityContainer _unityContainer;

        public static readonly IReadOnlyDictionary<Type, Type> ViewModelPageContainer =
            new Dictionary<Type, Type>()
        {
            {
                typeof(VacationListViewModel), typeof(VacationListView)
            }
        };

        public NavigationService(INavigation navigation, IUnityContainer unityContainer)
        {
            _navigation = navigation;
            _unityContainer = unityContainer;
        }

        public Task NavigateToAsync<TViewModelTo>(string args = null) where TViewModelTo : ViewModelBase
        {
            Type viewType;
            if (ViewModelPageContainer.TryGetValue(typeof (TViewModelTo), out viewType))
            {
                var resolvedView = (Page)Activator.CreateInstance(viewType);
                
                var viewModelForResolvedView = _unityContainer.Resolve<TViewModelTo>();
                resolvedView.BindingContext = viewModelForResolvedView;
                return _navigation.PushAsync(resolvedView);
            }
            throw new ArgumentException($"Page for ViewModel of type '{typeof (TViewModelTo)}' is not defined");
        }

        public void NavigateTo<TViewModelTo>(string args) where TViewModelTo : ViewModelBase
        {
            
        }
    }
}
