using Android.App;
using Android.OS;
using EpamVTSClient.BLL.ViewModels;
using EpamVTSClient.Core.Services.Localization;
using Microsoft.Practices.Unity;

namespace EpamVTSClientNative.Droid.Activities
{
    [Activity]
    public class VacationActivity : ActivityBase<VacationViewModel>
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            string text = Intent.GetStringExtra("args");
            SetContentView(Resource.Layout.VacationInfo);

            await ViewModel.LoadDataFrom(int.Parse(text));
            //Task.Run(() => ViewModel.LoadDataFrom(int.Parse(text))).Wait();

            var localizationService = Factory.UnityContainer.Resolve<ILocalizationService>();

            this.BindLabel(Resource.Id.VacationInfoEndDateLabel, localizationService.Localize("vacationEndDateLabel"));
            this.BindLabel(Resource.Id.VacationInfoStartDateLabel, localizationService.Localize("vacationStartDateLabel"));
            this.BindLabel(Resource.Id.VacationInfoStatusLabel, localizationService.Localize("vacationStatusInfoLabel"));
            this.BindLabel(Resource.Id.VacationInfoTypeLabel, localizationService.Localize("vacationTypeInfoLabel"));

            this.BindText(Resource.Id.VacationInfoEndDate, ViewModel, vm => vm.EndDate);
            this.BindText(Resource.Id.VacationInfoStartDate, ViewModel, vm => vm.StartDate);
            this.BindText(Resource.Id.VacationInfoStatus, ViewModel, vm => vm.VacationStatusToDisplay);
            this.BindText(Resource.Id.VacationInfoType, ViewModel, vm => vm.Type);

            InitSideMenu();
        }
    }
}