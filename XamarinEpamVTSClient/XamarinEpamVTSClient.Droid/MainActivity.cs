using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using EpamVTSCLient.Platform_Specific.Android;
using Java.IO;
using Java.Lang;

namespace XamarinEpamVTSClient.Droid
{
    [Activity(Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App(new AndroidRegistry()));

            var currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            AndroidEnvironment.UnhandledExceptionRaiser += (sender, args) =>
            {
                args.Handled = true;
                ShowAlertMessage();
            };
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            //Exception e = (Exception)unhandledExceptionEventArgs.ExceptionObject;
            //Console.WriteLine("Exception was thrown: " + e.Message);
            //ShowAlertMessage();
        }

        private void ShowAlertMessage()
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetTitle("Error");
            builder.SetMessage("Something went wrong. Please, try again later.");
            builder.SetCancelable(true);
            builder.SetPositiveButton("OK", delegate { Finish(); });
            builder.SetNegativeButton("Cancel", delegate { Finish(); });
            builder.Show();
        }
    }
}

