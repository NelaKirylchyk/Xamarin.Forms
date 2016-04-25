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
            string parsedText = Intent.GetStringExtra("args");
            SetContentView(Resource.Layout.VacationInfo);

            await ViewModel.LoadDataFrom(int.Parse(parsedText));
            //Task.Run(() => ViewModel.LoadDataFrom(int.Parse(text))).Wait();

            this.BindLabel(Resource.Id.VacationInfoEndDateLabel, LocalizationService.Localize("vacationEndDateLabel"));
            this.BindLabel(Resource.Id.VacationInfoStartDateLabel, LocalizationService.Localize("vacationStartDateLabel"));
            this.BindLabel(Resource.Id.VacationInfoStatusLabel, LocalizationService.Localize("vacationStatusInfoLabel"));
            this.BindLabel(Resource.Id.VacationInfoTypeLabel, LocalizationService.Localize("vacationTypeInfoLabel"));
            //this.BindLabel(Resource.Id.VacationInfoImageLabel, LocalizationService.Localize("vacationImageLabel"));
            this.BindLabel(Resource.Id.DeleteVacationBtn, LocalizationService.Localize("DeleteVacationBtn"));
            this.BindLabel(Resource.Id.EditVacationBtn, LocalizationService.Localize("EditVacationBtn"));

            this.BindText(Resource.Id.VacationInfoEndDate, ViewModel, vm => vm.EndDate);
            this.BindText(Resource.Id.VacationInfoStartDate, ViewModel, vm => vm.StartDate);
            this.BindText(Resource.Id.VacationInfoStatus, ViewModel, vm => vm.VacationStatusToDisplay);
            this.BindText(Resource.Id.VacationInfoType, ViewModel, vm => vm.Type);

            this.BindCommand(Resource.Id.DeleteVacationBtn, ViewModel.DeleteVacationCommand);
            this.BindCommand(Resource.Id.EditVacationBtn, ViewModel.NavigateToEditViewCommand);

            this.BindImageView(Resource.Id.VacationInfoPageImageView, ViewModel, vm => vm.VacationForm);

            InitSideMenu(LocalizationService.Localize("VacationInfoTitle"));
        }
    }
}