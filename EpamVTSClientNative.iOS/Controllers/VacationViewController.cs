using System;
using System.Collections.Generic;
using System.Linq;
using Cirrious.FluentLayouts.Touch;
using EpamVTSClient.BLL.ViewModels;
using EpamVTSClient.Core.Enums;
using EpamVTSClient.Core.Helpers;
using UIKit;

namespace EpamVTSClientNative.iOS.Controllers
{
    public class VacationViewController : BaseViewController<EditVacationViewModel>
    {
        UIButton showSimpleButton;
        UILabel dateLabel;

        private UIDatePicker _startDatePicker;
        private UIDatePicker _endDatePicker;

        private VacationStatusPickerViewModel _vacationStatusPickerViewModel;
        private VacationTypePickerViewModel _vacationTypePickerViewModel;

        private Dictionary<VacationStatus, string> _vacationStatusDictionary;
        private Dictionary<VacationType, string> _vacationTypeDictionary;

        protected override async void Initialize()
        {
            base.Initialize();
            int id;
            if (int.TryParse(args, out id))
            {
                await ViewModel.LoadDataFrom(id);
            }
            SetVacationInfo();

            _vacationStatusPickerViewModel = new VacationStatusPickerViewModel(_vacationStatusDictionary, ViewModel.VacationStatus);
            _vacationStatusPickerViewModel.ValueChanged += VacationStatusPickerViewModelOnValueChanged;

            _vacationTypePickerViewModel = new VacationTypePickerViewModel(_vacationTypeDictionary, ViewModel.VacationType);
            _vacationTypePickerViewModel.ValueChanged += VacationTypePickerViewModelOnValueChanged;

            var vacationStatusPicker = ControlsExtensions.SetUiPicker(_vacationStatusPickerViewModel);
            var vacationTypePicker = ControlsExtensions.SetUiPicker(_vacationStatusPickerViewModel);
            var vacationStatusLabel = ControlsExtensions.SetUiLabel("Statuses");
            var vacationTypeLabel = ControlsExtensions.SetUiLabel("Types");

            var startDateLabel = ControlsExtensions.SetUiLabel("Start Date");
            _startDatePicker = ControlsExtensions.SetDatePicker(ViewModel.StartDate);
            _startDatePicker.ValueChanged += StartDatePickerOnValueChanged;
            //_endDatePicker = ControlsExtensions.SetDatePicker(ViewModel.EndDate);
            //_endDatePicker.ValueChanged += EndDatePickerOnValueChanged;
            // vacationStatusPicker = new UIPickerView(new CoreGraphics.CGRect(10f, 244f, this.View.Frame.Width - 20, 250f));

            Add(vacationStatusLabel);
            Add(vacationStatusPicker);
            Add(vacationTypeLabel);
            Add(vacationTypePicker);
            Add(startDateLabel);
            Add(_startDatePicker);

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            View.InsertSubview(new UIImageView(UIImage.FromBundle("illustration")), 0);

            const int height = 46;
            const int labelMargin = 5;
            View.AddConstraints(
                vacationStatusLabel.AtTopOf(View, 50),
                //     vacationStatusLabel.Left().EqualTo().LeftOf(View),
                //      vacationStatusLabel.Right().EqualTo().RightOf(View),
                vacationStatusLabel.CenterX().EqualTo().CenterXOf(View),
               // vacationStatusLabel.CenterY().EqualTo().CenterYOf(View),

                vacationStatusPicker.Below(vacationStatusLabel, labelMargin),
                vacationStatusPicker.Height().EqualTo(height),

               vacationTypeLabel.Below(vacationStatusPicker, labelMargin),
                // vacationTypeLabel.Left().EqualTo().LeftOf(View),
                // vacationTypeLabel.Right().EqualTo().RightOf(View),
                vacationTypeLabel.CenterX().EqualTo().CenterXOf(View),
             //   vacationTypeLabel.CenterY().EqualTo().CenterYOf(View),

                vacationTypePicker.Below(vacationTypeLabel, labelMargin),
                vacationTypePicker.Height().EqualTo(height),
                

                startDateLabel.Below(vacationTypePicker, labelMargin),
                startDateLabel.CenterX().EqualTo().CenterXOf(View),

                _startDatePicker.Below(startDateLabel, labelMargin),
                _startDatePicker.Height().EqualTo(height));

        }

        private void SetVacationInfo()
        {
            List<VacationType> vacationTypes = EnumExtensions.GetValues<VacationType>().ToList();
            _vacationTypeDictionary = vacationTypes.ToDictionary(vacationType => vacationType,
                vacationType => LocalizationService.Localize(vacationType.ToString()));

            List<VacationStatus> vacationStatuses = EnumExtensions.GetValues<VacationStatus>().ToList();
            _vacationStatusDictionary = vacationStatuses.ToDictionary(vacationStatuse => vacationStatuse,
                vacationStatuse => LocalizationService.Localize(vacationStatuse.ToString()));
        }

        private void VacationTypePickerViewModelOnValueChanged(object sender, EventArgs eventArgs)
        {
            string selectedItem = _vacationTypePickerViewModel.SelectedItem;
            var vacationType = _vacationTypeDictionary.FirstOrDefault(r => r.Value == selectedItem).Key;
            ViewModel.VacationType = vacationType;
        }

        private void VacationStatusPickerViewModelOnValueChanged(object sender, EventArgs eventArgs)
        {
            string selectedItem = _vacationStatusPickerViewModel.SelectedItem;
            var vacationStatus = _vacationStatusDictionary.FirstOrDefault(r => r.Value == selectedItem).Key;
            ViewModel.VacationStatus = vacationStatus;
        }

        private void EndDatePickerOnValueChanged(object sender, EventArgs eventArgs)
        {
        }

        private void StartDatePickerOnValueChanged(object sender, EventArgs eventArgs)
        {
        }
    }
}