using System;
using System.Collections.Generic;
using System.Linq;
using Cirrious.FluentLayouts.Touch;
using EpamVTSClient.BLL.ViewModels;
using EpamVTSClient.Core.Enums;
using EpamVTSClient.Core.Helpers;
using EpamVTSClientNative.iOS.Controllers.UIPickerViewModels;
using EpamVTSClientNative.iOS.Helpers;
using Foundation;
using UIKit;

namespace EpamVTSClientNative.iOS.Controllers
{
    public class VacationViewController : BaseViewController<EditVacationViewModel>
    {
        private VacationStatusPickerViewModel _vacationStatusPickerViewModel;
        private VacationTypePickerViewModel _vacationTypePickerViewModel;
        private Dictionary<VacationStatus, string> _vacationStatusDictionary;
        private Dictionary<VacationType, string> _vacationTypeDictionary;
        private UIImagePickerController _imagePicker;
        private UIImageView _imageView;
        UIPickerView _vacationStatusPicker;
        UIPickerView _vacationTypePicker;
        UILabel _vacationStatusLabel;
        UILabel _vacationTypeLabel;
        UILabel _startDateLabel;
        UILabel _endDateLabel;
        UIDatePicker _startDatePicker;
        UIDatePicker _endDatePicker;
        UIButton _saveButton;
        UIButton _cancelButton;
        UIButton _choosePhotoButton;
        UIButton _cameraButton;

        protected override async void Initialize()
        {
            base.Initialize();

            SidebarController.Disabled = false;

            int id;
            if (int.TryParse(Args, out id))
            {
                await ViewModel.LoadDataFrom(id);
            }
            else
            {
                ViewModel.SetDefaultData();
            }
            SetVacationInfo();

            _vacationStatusPicker = ControlsExtensions.SetUiPicker(_vacationStatusPickerViewModel, _vacationStatusPickerViewModel.SelectedIndex);
            _vacationTypePicker = ControlsExtensions.SetUiPicker(_vacationTypePickerViewModel, _vacationTypePickerViewModel.SelectedIndex);
            _vacationStatusLabel = ControlsExtensions.SetUiLabel(LocalizationService.Localize("vacationStatusInfoLabel"));
            _vacationTypeLabel = ControlsExtensions.SetUiLabel(LocalizationService.Localize("vacationTypeInfoLabel"));
            _startDateLabel = ControlsExtensions.SetUiLabel(LocalizationService.Localize("vacationStartDateLabel"));
            _endDateLabel = ControlsExtensions.SetUiLabel(LocalizationService.Localize("vacationEndDateLabel"));
            _startDatePicker = ControlsExtensions.SetDatePicker(ViewModel.StartDate);
            _startDatePicker.ValueChanged += StartDatePickerOnValueChanged;
            _endDatePicker = ControlsExtensions.SetDatePicker(ViewModel.EndDate);
            _endDatePicker.ValueChanged += EndDatePickerOnValueChanged;
            _saveButton = ControlsExtensions.SetButton(LocalizationService.Localize("SaveEditVacationBtn"));
            _saveButton.TouchUpInside += SaveButtonOnTouchUpInside;
            _cancelButton = ControlsExtensions.SetButton(LocalizationService.Localize("CancelEditVacationBtn"));
            _cancelButton.TouchUpInside += CancelButtonOnTouchUpInside;
            _choosePhotoButton = ControlsExtensions.SetButton(LocalizationService.Localize("vacationImageLabel"));
            _choosePhotoButton.TouchUpInside += ChoosePhotoButtonOnTouchUpInside;
            _cameraButton = ControlsExtensions.SetButton(LocalizationService.Localize("openCameraBtn"));
            _cameraButton.TouchUpInside += CameraButtonOnTouchUpInside;
            _imageView = new UIImageView();

            if (!string.IsNullOrEmpty(ViewModel.VacationForm))
            {
                _imageView.Image = ImageHelper.Base64ToImage(ViewModel.VacationForm);
            }

            Add(_vacationStatusLabel);
            Add(_vacationStatusPicker);
            Add(_vacationTypeLabel);
            Add(_vacationTypePicker);
            Add(_startDateLabel);
            Add(_startDatePicker);
            Add(_endDateLabel);
            Add(_endDatePicker);
            Add(_choosePhotoButton);
            Add(_cameraButton);
            Add(_imageView);
            Add(_saveButton);
            Add(_cancelButton);

            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            View.InsertSubview(new UIImageView(UIImage.FromBundle("illustration")), 0);

            AddConstraints();
        }

        private void AddConstraints()
        {
            const int height = 46;
            const int labelMargin = 10;
            const int rightColumnElementWidth = 210;
            const int buttonWidth = 100;
            View.AddConstraints(
                _vacationStatusLabel.AtTopOf(View, labelMargin),
                _vacationStatusLabel.AtLeftOf(View),
                _vacationStatusLabel.Height().EqualTo(height),

                _vacationStatusPicker.AtRightOf(View),
                _vacationStatusPicker.AtTopOf(View, 10),
                _vacationStatusPicker.Width().EqualTo(rightColumnElementWidth),
                _vacationStatusPicker.Height().EqualTo(height),

                _vacationTypeLabel.Below(_vacationStatusPicker, labelMargin),
                _vacationTypeLabel.AtLeftOf(View),

                _vacationTypePicker.Below(_vacationStatusPicker, labelMargin),
                _vacationTypePicker.Height().EqualTo(height),
                _vacationTypePicker.AtRightOf(View),
                _vacationTypePicker.Width().EqualTo(rightColumnElementWidth),

                _startDateLabel.Below(_vacationTypePicker, labelMargin),
                _startDateLabel.AtLeftOf(View),

                _startDatePicker.Below(_vacationTypePicker, labelMargin),
                _startDatePicker.AtRightOf(View),
                _startDatePicker.Width().EqualTo(rightColumnElementWidth),
                _startDatePicker.Height().EqualTo(height),

                _endDateLabel.Below(_startDatePicker, labelMargin),
                _endDateLabel.AtLeftOf(View),

                _endDatePicker.Below(_startDatePicker, labelMargin),
                _endDatePicker.Height().EqualTo(height),
                _endDatePicker.AtRightOf(View),
                _endDatePicker.Width().EqualTo(rightColumnElementWidth),

                _imageView.Below(_endDatePicker, labelMargin),
                _imageView.Width().EqualTo(buttonWidth),
                _imageView.Height().EqualTo(buttonWidth),

                _choosePhotoButton.Below(_imageView, labelMargin),
                _choosePhotoButton.AtLeftOf(View, labelMargin),
                _choosePhotoButton.Width().EqualTo(buttonWidth),

                _cameraButton.Below(_imageView, labelMargin),
                _cameraButton.AtRightOf(View, labelMargin),
                _cameraButton.Width().EqualTo(buttonWidth),

                _saveButton.Below(_cameraButton, labelMargin),
                _saveButton.AtLeftOf(View, labelMargin),
                _saveButton.Width().EqualTo(buttonWidth),

                _cancelButton.Below(_cameraButton, labelMargin),
                _cancelButton.Width().EqualTo(buttonWidth),
                _cancelButton.AtRightOf(View, labelMargin)
                );
        }

        private void CameraButtonOnTouchUpInside(object sender, EventArgs eventArgs)
        {
            Camera.Camera.TakePicture(this, (obj) =>
            {
                var photo = obj.ValueForKey(new NSString("UIImagePickerControllerOriginalImage")) as UIImage;
                if (photo != null)
                {
                    string encodedString = photo.AsJPEG(0.23f).GetBase64EncodedString(NSDataBase64EncodingOptions.None);
                    ViewModel.VacationForm = encodedString;
                }
            });
        }

        private void ChoosePhotoButtonOnTouchUpInside(object sender, EventArgs eventArgs)
        {
            _imagePicker = new UIImagePickerController
            {
                SourceType = UIImagePickerControllerSourceType.PhotoLibrary,
                MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary)
            };
            _imagePicker.FinishedPickingMedia += ImagePickerOnFinishedPickingMedia;
            _imagePicker.Canceled += ImagePickerOnCanceled;
            PresentModalViewController(_imagePicker, true);
        }

        private void ImagePickerOnCanceled(object sender, EventArgs e)
        {
            _imagePicker.DismissModalViewController(true);
        }

        private void ImagePickerOnFinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
        {
            bool isImage = false;
            switch (e.Info[UIImagePickerController.MediaType].ToString())
            {
                case "public.image":
                    isImage = true;
                    break;
                case "public.video":
                    break;
            }

            NSUrl referenceUrl = e.Info[new NSString("UIImagePickerControllerReferenceUrl")] as NSUrl;
            if (referenceUrl != null)
                Console.WriteLine(referenceUrl.ToString());
            if (isImage)
            {
                UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
                if (originalImage != null)
                {
                    _imageView.Image = originalImage;

                    string encodedString = _imageView.Image.AsJPEG(0.23f).GetBase64EncodedString(NSDataBase64EncodingOptions.None);
                    ViewModel.VacationForm = encodedString;
                }
                UIImage editedImage = e.Info[UIImagePickerController.EditedImage] as UIImage;
                if (editedImage != null)
                {
                    _imageView.Image = editedImage;
                }
            }
            else
            {
                NSUrl mediaUrl = e.Info[UIImagePickerController.MediaURL] as NSUrl;
                if (mediaUrl != null)
                {
                    Console.WriteLine(mediaUrl.ToString());
                }
            }
            _imagePicker.DismissModalViewController(true);
        }

        private void CancelButtonOnTouchUpInside(object sender, EventArgs eventArgs)
        {
            ViewModel.CancelEditVacationCommand.Execute(null);
        }

        private void SaveButtonOnTouchUpInside(object sender, EventArgs eventArgs)
        {
            DismissModalViewController(true);
            ViewModel.EditVacationCommand.Execute(null);
        }

        private void SetVacationInfo()
        {
            List<VacationType> vacationTypes = EnumExtensions.GetValues<VacationType>().ToList();
            _vacationTypeDictionary = vacationTypes.ToDictionary(vacationType => vacationType,
                vacationType => LocalizationService.Localize(vacationType.ToString()));

            List<VacationStatus> vacationStatuses = EnumExtensions.GetValues<VacationStatus>().ToList();
            _vacationStatusDictionary = vacationStatuses.ToDictionary(vacationStatuse => vacationStatuse,
                vacationStatuse => LocalizationService.Localize(vacationStatuse.ToString()));

            _vacationStatusPickerViewModel = new VacationStatusPickerViewModel(_vacationStatusDictionary, ViewModel.VacationStatus);
            _vacationStatusPickerViewModel.ValueChanged += VacationStatusPickerViewModelOnValueChanged;

            _vacationTypePickerViewModel = new VacationTypePickerViewModel(_vacationTypeDictionary, ViewModel.VacationType);
            _vacationTypePickerViewModel.ValueChanged += VacationTypePickerViewModelOnValueChanged;
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
            var uiDatePicker = sender as UIDatePicker;
            if (uiDatePicker != null)
            {
                ViewModel.EndDate = uiDatePicker.Date.NsDateToDateTime();
            }
        }

        private void StartDatePickerOnValueChanged(object sender, EventArgs eventArgs)
        {
            var uiDatePicker = sender as UIDatePicker;
            if (uiDatePicker != null)
            {
                ViewModel.StartDate = uiDatePicker.Date.NsDateToDateTime();
            }
        }
    }
}