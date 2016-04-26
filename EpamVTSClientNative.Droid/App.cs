using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using EpamVTSClient.BLL;
using Plugin.CurrentActivity;

namespace EpamVTSClientNative.Droid
{
    [Application]
    public sealed class App : Application, Application.IActivityLifecycleCallbacks
    {
        public App(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
        {
            RegisterActivityLifecycleCallbacks(this);
        }

        public override void OnCreate()
        {
            base.OnCreate();

            Factory.Init();
            DatabaseInitializer.Initialize(Factory.UnityContainer);
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityStopped(Activity activity)
        {
        }
    }
}