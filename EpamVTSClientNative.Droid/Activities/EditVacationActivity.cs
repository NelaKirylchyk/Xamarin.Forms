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
    public class EditVacationActivity : ActivityBase<EditVacationViewModel>
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            string parsedText = Intent.GetStringExtra("args");
            SetContentView(Resource.Layout.AddUpdateVacationInfo);
            string pageTitle;
            

            List<VacationType> vacationTypes = EnumExtensions.GetValues<VacationType>().ToList();
            List<VacationStatus> vacationStatuses = EnumExtensions.GetValues<VacationStatus>().ToList();

            if (parsedText == "add")
            {
                pageTitle = "VacationAddVacTitle";
                ViewModel.SetDefaultData();
                this.BindText(Resource.Id.StartDateEditInfo, ViewModel, vm => vm.StartDate);
                this.BindCommand(Resource.Id.SaveEditVacationBtn, ViewModel.EditVacationCommand);
                this.BindCommand(Resource.Id.CancelEditVacationBtn, ViewModel.CancelEditVacationCommand);

                this.BindSpinner(Resource.Id.VacTypeSpinner, ViewModel, vm => vm.VacationType, vacationTypes);
                this.BindSpinner(Resource.Id.VacStatusSpinner, ViewModel, vm => vm.VacationStatus, vacationStatuses);
                this.BindDatePicker(Resource.Id.EndDatePicker, ViewModel, vm => vm.EndDate);
            }
            else
            {
                await ViewModel.LoadDataFrom(int.Parse(parsedText));
                pageTitle = "VacationEditInfoTitle";

                this.BindText(Resource.Id.StartDateEditInfo, ViewModel, vm => vm.StartDate);
                this.BindCommand(Resource.Id.SaveEditVacationBtn, ViewModel.EditVacationCommand);
                this.BindCommand(Resource.Id.CancelEditVacationBtn, ViewModel.CancelEditVacationCommand);

                this.BindSpinner(Resource.Id.VacTypeSpinner, ViewModel, vm => vm.VacationType, vacationTypes);
                this.BindSpinner(Resource.Id.VacStatusSpinner, ViewModel, vm => vm.VacationStatus, vacationStatuses);
                this.BindDatePicker(Resource.Id.EndDatePicker, ViewModel, vm => vm.EndDate);
            }


            this.BindLabel(Resource.Id.SaveEditVacationBtn, LocalizationService.Localize("SaveEditVacationBtn"));
            this.BindLabel(Resource.Id.CancelEditVacationBtn, LocalizationService.Localize("CancelEditVacationBtn"));
            this.BindLabel(Resource.Id.VacEditStatusInfo, LocalizationService.Localize("vacationStatusInfoLabel"));
            this.BindLabel(Resource.Id.VacEditTypeInfo, LocalizationService.Localize("vacationTypeInfoLabel"));
            this.BindLabel(Resource.Id.VacEditStartDateInfo, LocalizationService.Localize("vacationStartDateLabel"));
            this.BindLabel(Resource.Id.VacEditEndDateInfo, LocalizationService.Localize("vacationEndDateLabel"));

            InitSideMenu(LocalizationService.Localize(pageTitle));
        }
    }
}