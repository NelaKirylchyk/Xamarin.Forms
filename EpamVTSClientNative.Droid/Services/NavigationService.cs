using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Content;
using EpamVTSClient.BLL.Services;
using EpamVTSClient.BLL.ViewModels;
using EpamVTSClient.BLL.ViewModels.Base;
using Microsoft.Practices.Unity;

namespace EpamVTSClientNative.Droid.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IUnityContainer _unityContainer;

        public static readonly IReadOnlyDictionary<Type, Type> ViewModelPageContainer =
            new Dictionary<Type, Type>()
        {
            {
                typeof(VacationListViewModel), typeof(VacationListActivity)
            }
        };

        public NavigationService(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public Task NavigateToAsync<TViewModelTo>() where TViewModelTo : ViewModelBase
        {
            Type viewType;
            if (ViewModelPageContainer.TryGetValue(typeof (TViewModelTo), out viewType))
            {
                // var resolvedView = (Page)Activator.CreateInstance(viewType);

               // var resolvedViewModel = _unityContainer.Resolve<TViewModelTo>();

                var currentActivity = App.CurrentActivity;
                var intent = new Intent(currentActivity, viewType);
                currentActivity.StartActivity(intent);

                
                //resolvedView.BindingContext = viewModelForResolvedView;
                //return _navigation.PushAsync(resolvedView);
            }
            throw new ArgumentException($"Activity for ViewModel of type '{typeof (TViewModelTo)}' is not defined");
        }
    }
}
