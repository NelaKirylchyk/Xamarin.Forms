using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Widget;
using EpamVTSClient.BLL.ViewModels;
using EpamVTSClient.Core.Enums;
using EpamVTSClient.Core.Helpers;
using EpamVTSClientNative.Droid.Activities.Extensions;
using Uri = Android.Net.Uri;
using Environment = Android.OS.Environment;
using File = Java.IO.File;


namespace EpamVTSClientNative.Droid.Activities
{
    [Activity]
    public class EditVacationActivity : ActivityBase<EditVacationViewModel>
    {
        public static readonly int PickImageId = 1000;
        private ImageView _imageView;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddUpdateVacationInfo);

            string arguments = Intent.GetStringExtra("args");
            string pageTitle;
            if (string.Equals(arguments, "add", StringComparison.OrdinalIgnoreCase))
            {
                pageTitle = "VacationAddVacTitle";
                ViewModel.SetDefaultData();
            }
            else
            {
                await ViewModel.LoadDataFrom(int.Parse(arguments));
                pageTitle = "VacationEditInfoTitle";
            }

            BindDatePicker();
            BindSpinners();
            BindCommands();
            BindLabels();
            BindImageControls();

            InitSideMenu(LocalizationService.Localize(pageTitle));
        }

        private void BindImageControls()
        {
            _imageView = FindViewById<ImageView>(Resource.Id.editPageImageView);
            Button imageUploadButton = FindViewById<Button>(Resource.Id.AttachmentBtn);
            imageUploadButton.Click += ImageUploadButtonOnClick;
            this.BindImageView(Resource.Id.editPageImageView, ViewModel, vm => vm.VacationForm);

            if (IsThereAnAppToTakePictures())
            {
                CreateDirectoryForPictures();
                Button openCameraButton = FindViewById<Button>(Resource.Id.openCameraBtn);
                openCameraButton.Click += OpenCameraButtonOnClick;
            }
        }

        private void BindDatePicker()
        {
            this.BindDatePicker(Resource.Id.EndDatePicker, ViewModel, vm => vm.EndDate);
        }

        private void BindSpinners()
        {
            List<VacationType> vacationTypes = EnumExtensions.GetValues<VacationType>().ToList();
            List<VacationStatus> vacationStatuses = EnumExtensions.GetValues<VacationStatus>().ToList();
            this.BindSpinner(Resource.Id.VacTypeSpinner, ViewModel, vm => vm.VacationType, vacationTypes);
            this.BindSpinner(Resource.Id.VacStatusSpinner, ViewModel, vm => vm.VacationStatus, vacationStatuses);
        }

        private void BindCommands()
        {
            this.BindCommand(Resource.Id.SaveEditVacationBtn, ViewModel.EditVacationCommand);
            this.BindCommand(Resource.Id.CancelEditVacationBtn, ViewModel.CancelEditVacationCommand);
        }

        private void BindLabels()
        {
            this.BindText(Resource.Id.StartDateEditInfo, ViewModel, vm => vm.StartDate);
            this.BindLabel(Resource.Id.SaveEditVacationBtn, LocalizationService.Localize("SaveEditVacationBtn"));
            this.BindLabel(Resource.Id.CancelEditVacationBtn, LocalizationService.Localize("CancelEditVacationBtn"));
            this.BindLabel(Resource.Id.VacEditStatusInfo, LocalizationService.Localize("vacationStatusInfoLabel"));
            this.BindLabel(Resource.Id.VacEditTypeInfo, LocalizationService.Localize("vacationTypeInfoLabel"));
            this.BindLabel(Resource.Id.VacEditStartDateInfo, LocalizationService.Localize("vacationStartDateLabel"));
            this.BindLabel(Resource.Id.VacEditEndDateInfo, LocalizationService.Localize("vacationEndDateLabel"));
            this.BindLabel(Resource.Id.AttachmentBtn, LocalizationService.Localize("vacationImageLabel"));
            this.BindLabel(Resource.Id.openCameraBtn, LocalizationService.Localize("openCameraBtn"));
        }

        private void ImageUploadButtonOnClick(object sender, EventArgs eventArgs)
        {
            Intent = new Intent();
            Intent.SetType("image/*");
            Intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if ((requestCode == PickImageId) && (resultCode == Result.Ok) && (data != null))
            {
                Uri uri = data.Data;
                _imageView.SetImageURI(uri);

                Uri currentImageUri = Uri.Parse(data.DataString);
                Bitmap bitmap = BitmapFactory.DecodeStream(ContentResolver.OpenInputStream(currentImageUri));

                string base64String = BitmapHelper.BitmapToBase64String(bitmap);
                ViewModel.VacationForm = base64String;
            }
            else
            {
                Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                Uri contentUri = Uri.FromFile(CameraHelper.File);
                mediaScanIntent.SetData(contentUri);
                SendBroadcast(mediaScanIntent);

                // Display in ImageView. We will resize the bitmap to fit the display.
                // Loading the full sized image will consume to much memory
                // and cause the application to crash.

                int height = Resources.DisplayMetrics.HeightPixels;
                int width = _imageView.Height;
                CameraHelper.Bitmap = CameraHelper.File.Path.LoadAndResizeBitmap(width, height);
                if (CameraHelper.Bitmap != null)
                {
                    _imageView.SetImageBitmap(CameraHelper.Bitmap);

                    string base64String = BitmapHelper.BitmapToBase64String(CameraHelper.Bitmap);
                    ViewModel.VacationForm = base64String;

                    CameraHelper.Bitmap = null;
                }
                // Dispose of the Java side bitmap.
                GC.Collect();
            }
        }

        private void CreateDirectoryForPictures()
        {
            CameraHelper.Dir = new File(
                Environment.GetExternalStoragePublicDirectory(
                    Environment.DirectoryPictures), "CameraAppDemo");
            if (!CameraHelper.Dir.Exists())
            {
                CameraHelper.Dir.Mkdirs();
            }
        }

        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        private void OpenCameraButtonOnClick(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            CameraHelper.File = new File(CameraHelper.Dir, $"myPhoto_{Guid.NewGuid()}.jpg");
            intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(CameraHelper.File));
            StartActivityForResult(intent, 0);
        }
    }
}