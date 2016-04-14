using System;
using EpamVTSClient.BLL;
using EpamVTSClient.BLL.ViewModels;
using EpamVTSClient.Core;
using Microsoft.Practices.Unity;
using Xamarin.Forms;
using XamarinEpamVTSClient.Views;

namespace XamarinEpamVTSClient
{
    public class App : Application
    {
        public App(params IUnityContainerRegistry[] registries)
        {
            var loginPageView = new LoginPageView();
            MainPage = new NavigationPage(loginPageView);
            
            Factory.Init(registries);
            DatabaseInitializer.Initialize(Factory.UnityContainer);

            var loginPageViewModel = Factory.UnityContainer.Resolve<LoginPageViewModel>();
            loginPageView.BindingContext = loginPageViewModel;
            loginPageViewModel.UserName = "dz@epam.com";
            loginPageViewModel.Password = "test1";

            EpamVTSClient.Core.Resx.AppResources.Culture = GetInstance<ILocalize>().GetCurrentCultureInfo();
        }

        protected T GetInstance<T>(string key = null)
        {
            return (T)GetInstance(typeof(T), key);
        }

        protected object GetInstance(Type service, string key)
        {
            return Factory.UnityContainer.Resolve(service, key);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
