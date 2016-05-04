using System;
using System.Collections.Generic;
using System.Linq;
using Cirrious.FluentLayouts.Touch;
using EpamVTSClient.BLL.ViewModels;
using EpamVTSClient.Core.Enums;
using EpamVTSClient.Core.Helpers;
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

        protected override async void Initialize()
        {
            base.Initialize();
            int id;
            if (int.TryParse(args, out id))
            {
                await ViewModel.LoadDataFrom(id);
            }
            else
            {
                ViewModel.SetDefaultData();
            }
            SetVacationInfo();

            var vacationStatusPicker = ControlsExtensions.SetUiPicker(_vacationStatusPickerViewModel, _vacationStatusPickerViewModel.SelectedIndex);
            var vacationTypePicker = ControlsExtensions.SetUiPicker(_vacationTypePickerViewModel, _vacationTypePickerViewModel.SelectedIndex);

            var vacationStatusLabel = ControlsExtensions.SetUiLabel("Status");
            var vacationTypeLabel = ControlsExtensions.SetUiLabel("Type");
            var startDateLabel = ControlsExtensions.SetUiLabel("Start Date");
            var endDateLabel = ControlsExtensions.SetUiLabel("End Date");

            var startDatePicker = ControlsExtensions.SetDatePicker(ViewModel.StartDate);
            startDatePicker.ValueChanged += StartDatePickerOnValueChanged;
            var endDatePicker = ControlsExtensions.SetDatePicker(ViewModel.EndDate);
            endDatePicker.ValueChanged += EndDatePickerOnValueChanged;

            UIButton saveButton = ControlsExtensions.SetButton("Save");
            saveButton.TouchUpInside += SaveButtonOnTouchUpInside;
            UIButton cancelButton = ControlsExtensions.SetButton("Cancel");
            cancelButton.TouchUpInside += CancelButtonOnTouchUpInside;
            UIButton choosePhotoButton = ControlsExtensions.SetButton("Pick a photo");
            choosePhotoButton.TouchUpInside += ChoosePhotoButtonOnTouchUpInside;
            var cameraButton = ControlsExtensions.SetButton("Camera");
            cameraButton.TouchUpInside += CameraButtonOnTouchUpInside;

            _imageView = new UIImageView();
            if (!string.IsNullOrEmpty(ViewModel.VacationForm))
            {
                Base64ToImage(ViewModel.VacationForm);
            }

            Add(vacationStatusLabel);
            Add(vacationStatusPicker);
            Add(vacationTypeLabel);
            Add(vacationTypePicker);
            Add(startDateLabel);
            Add(startDatePicker);
            Add(endDateLabel);
            Add(endDatePicker);
            Add(choosePhotoButton);
            Add(cameraButton);
            Add(_imageView);
            Add(saveButton);
            Add(cancelButton);


            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            View.InsertSubview(new UIImageView(UIImage.FromBundle("illustration")), 0);

            const int height = 46;
            const int labelMargin = 5;
            View.AddConstraints(
                vacationStatusLabel.AtTopOf(View, 10),
                //vacationStatusLabel.Left().EqualTo().LeftOf(View),
                //vacationStatusLabel.Right().EqualTo().RightOf(View),
                vacationStatusLabel.AtLeftOf(View),
                vacationStatusLabel.Height().EqualTo(height),
               // vacationStatusLabel.CenterX().EqualTo().CenterXOf(View),
                // vacationStatusLabel.CenterY().EqualTo().CenterYOf(View),

                //vacationStatusPicker.Below(vacationStatusLabel, labelMargin),
               // vacationStatusPicker.Height().EqualTo(height),
               vacationStatusPicker.AtRightOf(View),
               vacationStatusPicker.AtTopOf(View, 10),
               vacationStatusPicker.Width().EqualTo(200),
              // vacationStatusPicker.AtTopOf(View, 10),
                vacationStatusPicker.Height().EqualTo(height),
                

                vacationTypeLabel.Below(vacationStatusPicker, labelMargin),
                //vacationTypeLabel.Left().EqualTo().LeftOf(View),
                //vacationTypeLabel.Right().EqualTo().RightOf(View),
                vacationTypeLabel.CenterX().EqualTo().CenterXOf(View),
                //vacationTypeLabel.CenterY().EqualTo().CenterYOf(View),

                vacationTypePicker.Below(vacationTypeLabel, labelMargin),
                vacationTypePicker.Height().EqualTo(height),

                startDateLabel.Below(vacationTypePicker, labelMargin),
                startDateLabel.CenterX().EqualTo().CenterXOf(View),

                startDatePicker.Below(startDateLabel, labelMargin),
                startDatePicker.Height().EqualTo(height),

                endDateLabel.Below(startDatePicker, labelMargin),
                endDateLabel.CenterX().EqualTo().CenterXOf(View),

                endDatePicker.Below(endDateLabel, labelMargin),
                endDatePicker.Height().EqualTo(height),

                _imageView.Below(endDatePicker, labelMargin),
                _imageView.Width().EqualTo(50),
                _imageView.Height().EqualTo(50),

                choosePhotoButton.Below(_imageView, labelMargin),
                choosePhotoButton.AtLeftOf(View, 50),

                cameraButton.Below(_imageView, labelMargin),
                cameraButton.AtRightOf(View, 50),

                saveButton.Below(cameraButton, labelMargin),
                saveButton.AtLeftOf(View, 50),
                saveButton.Width().EqualTo(100),

                cancelButton.Below(cameraButton, labelMargin),
                cancelButton.Width().EqualTo(100),
                cancelButton.AtRightOf(View, 50)
                );
        }

        private void CameraButtonOnTouchUpInside(object sender, EventArgs eventArgs)
        {
            Camera.TakePicture(this, (obj) =>
            {
                var photo = obj.ValueForKey(new NSString("UIImagePickerControllerOriginalImage")) as UIImage;
                if (photo != null)
                {
                    string encodedString = photo.AsJPEG(0.23f).GetBase64EncodedString(NSDataBase64EncodingOptions.None);
                    ViewModel.VacationForm = encodedString;
                }
            });
        }

        private void Base64ToImage(string base64)
        {
            byte[] encodedDataAsBytes = Convert.FromBase64String(base64);
            NSData imageData = NSData.FromArray(encodedDataAsBytes);
            var img = UIImage.LoadFromData(imageData);
            _imageView.Image = img;
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

        void ImagePickerOnCanceled(object sender, EventArgs e)
        {
            _imagePicker.DismissModalViewController(true);
        }

        protected void ImagePickerOnFinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
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
                NSDate nsDate = uiDatePicker.Date;
                var nsDateToDateTime = NsDateToDateTime(nsDate);
                ViewModel.EndDate = nsDateToDateTime;
            }
        }

        private void StartDatePickerOnValueChanged(object sender, EventArgs eventArgs)
        {
            var uiDatePicker = sender as UIDatePicker;
            if (uiDatePicker != null)
            {
                NSDate nsDate = uiDatePicker.Date;
                var nsDateToDateTime = NsDateToDateTime(nsDate);
                ViewModel.StartDate = nsDateToDateTime;
            }
        }

        public static DateTime NsDateToDateTime(NSDate date)
        {
            DateTime reference = new DateTime(2001, 1, 1, 0, 0, 0);
            DateTime currentDate = reference.AddSeconds(date.SecondsSinceReferenceDate);
            DateTime localDate = currentDate.ToLocalTime();
            return localDate;
        }
    }
}