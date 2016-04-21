using Android.App;
using Android.OS;
using Android.Support.V7.App;
using EpamVTSClient.BLL.ViewModels.Base;
using Microsoft.Practices.Unity;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using EpamVTSClient.BLL.Services;
using EpamVTSClient.BLL.ViewModels;

namespace EpamVTSClientNative.Droid.Activities
{
    [Activity]
    public class ActivityBase<TViewModel> : ActionBarActivity where TViewModel: ViewModelBase
    {
        private DrawerLayout _drawerLayout;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ViewModel = Factory.UnityContainer.Resolve<TViewModel>();
        }

        protected void InitSideMenu()
        {
            _drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            // Init toolbar
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            // Attach item selected handler to navigation view
            var navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;

            // Create ActionBarDrawerToggle button and add it to the toolbar
            var drawerToggle = new ActionBarDrawerToggle(this, _drawerLayout, toolbar, Resource.String.open_drawer,
                Resource.String.close_drawer);
            _drawerLayout.SetDrawerListener(drawerToggle);
            drawerToggle.SyncState();
        }

        void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            var navigationService = Factory.UnityContainer.Resolve<INavigationService>();
            switch (e.MenuItem.ItemId)
            {
                case (Resource.Id.nav_vacList):
                    navigationService.NavigateTo<VacationListViewModel>(null);
                    break;
                case (Resource.Id.nav_addVac):
                    // React on 'Messages' selection
                    break;
            }

            // Close drawer
            _drawerLayout.CloseDrawers();
        }

        public TViewModel ViewModel { get; private set; }
    }
}