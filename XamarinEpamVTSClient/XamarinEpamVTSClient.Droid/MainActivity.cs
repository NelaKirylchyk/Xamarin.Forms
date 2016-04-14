using System;
using Android.App;
using Android.Content.PM;
using Android.OS;

namespace XamarinEpamVTSClient.Droid
{
    [Activity(Label = "XamarinEpamVTSClient", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            //LoadApplication(new App());
            LoadApplication(new App(new AndroidRegistry()));

            var currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += CurrentDomainOnUnhandledException;
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            Exception e = (Exception)unhandledExceptionEventArgs.ExceptionObject;
            Console.WriteLine("Exception was thrown: " + e.Message);
        }
    }
}

