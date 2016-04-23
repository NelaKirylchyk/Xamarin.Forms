using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using EpamVTSClient.BLL.ViewModels;
using EpamVTSClient.Core.Enums;
using EpamVTSClient.Core.Helpers;
using EpamVTSClient.Core.Services.Localization;
using Microsoft.Practices.Unity;

namespace EpamVTSClientNative.Droid.Activities
{
    [Activity]
    public class AddVacationActivity : ActivityBase<EditVacationViewModel>
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            string parsedText = Intent.GetStringExtra("args");
            SetContentView(Resource.Layout.AddUpdateVacationInfo);
            string pageTitle;
            if (parsedText == "add")
            {
                pageTitle = "VacationAddVacTitle";
            }
            else
            {
                pageTitle = "VacationEditInfoTitle";
                await ViewModel.LoadDataFrom(int.Parse(parsedText));
            }

            this.BindText(Resource.Id.StartDateEditInfo, ViewModel, vm => vm.StartDate);

            this.BindCommand(Resource.Id.SaveEditVacationBtn, ViewModel.EditVacationCommand);
            this.BindCommand(Resource.Id.CancelEditVacationBtn, ViewModel.CancelEditVacationCommand);
            this.BindLabel(Resource.Id.SaveEditVacationBtn, LocalizationService.Localize("SaveEditVacationBtn"));
            this.BindLabel(Resource.Id.CancelEditVacationBtn, LocalizationService.Localize("CancelEditVacationBtn"));
            this.BindLabel(Resource.Id.VacEditStatusInfo, LocalizationService.Localize("vacationStatusInfoLabel"));
            this.BindLabel(Resource.Id.VacEditTypeInfo, LocalizationService.Localize("vacationTypeInfoLabel"));

            this.BindLabel(Resource.Id.VacEditStartDateInfo, LocalizationService.Localize("vacationStartDateLabel"));
            this.BindLabel(Resource.Id.VacEditEndDateInfo, LocalizationService.Localize("vacationEndDateLabel"));

            var localization = Factory.UnityContainer.Resolve<ILocalizationService>();
            List<VacationType> vacationTypes = EnumExtensions.GetValues<VacationType>().ToList();
            List<VacationStatus> vacationStatuses = EnumExtensions.GetValues<VacationStatus>().ToList();

            this.BindSpinner(Resource.Id.VacTypeSpinner, ViewModel, vm => vm.VacationType, vacationTypes);
            this.BindSpinner(Resource.Id.VacStatusSpinner, ViewModel, vm => vm.VacationStatus, vacationStatuses);
            this.BindDatePicker(Resource.Id.EndDatePicker, ViewModel, vm => vm.EndDate);


            InitSideMenu(LocalizationService.Localize(pageTitle));
        }
    }
}