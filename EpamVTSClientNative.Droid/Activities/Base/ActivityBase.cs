using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using EpamVTSClient.BLL.Services;
using EpamVTSClient.BLL.ViewModels;
using EpamVTSClient.BLL.ViewModels.Base;
using EpamVTSClient.Core.Services.Localization;
using EpamVTSClientNative.Droid.Activities.Extensions;
using Microsoft.Practices.Unity;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace EpamVTSClientNative.Droid.Activities.Base
{
    [Activity]
    public class ActivityBase<TViewModel> : ActionBarActivity where TViewModel: ViewModelBase
    {
        private DrawerLayout _drawerLayout;

        protected ILocalizationService LocalizationService { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            LocalizationService = Factory.UnityContainer.Resolve<ILocalizationService>();
            ViewModel = Factory.UnityContainer.Resolve<TViewModel>();
        }

        protected void InitSideMenu(string title)
        {
            _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            // Init toolbar
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.Title = title;
            SetSupportActionBar(toolbar);

            // Attach item selected handler to navigation view
            var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;

            // Create ActionBarDrawerToggle button and add it to the toolbar
            var drawerToggle = new ActionBarDrawerToggle(this, _drawerLayout, toolbar, Resource.String.open_drawer,
                Resource.String.close_drawer);
            _drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();

            this.BindNavMenu(Resource.Id.nav_vacList, LocalizationService.Localize("vacationList"));
            this.BindNavMenu(Resource.Id.nav_addVac, LocalizationService.Localize("addVacation"));
            this.BindLabel(Resource.Id.NavMenuTitle, LocalizationService.Localize("menu"));

        }

        async void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            var navigationService = Factory.UnityContainer.Resolve<INavigationService>();
            switch (e.MenuItem.ItemId)
            {
                case (Resource.Id.nav_vacList):
                    await navigationService.NavigateToAsync<VacationListViewModel>(null);
                    break;
                case (Resource.Id.nav_addVac):
                    await navigationService.NavigateToAsync<EditVacationViewModel>("add");
                    break;
            }

            // Close drawer
            _drawerLayout.CloseDrawers();
        }

        public TViewModel ViewModel { get; private set; }
    }
}