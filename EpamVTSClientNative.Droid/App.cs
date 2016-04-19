using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Plugin.CurrentActivity;

namespace EpamVTSClientNative.Droid
{
    [Application]
    public class App : Application, Application.IActivityLifecycleCallbacks
    {
        public static Activity CurrentActivity { get; set; }
        public App(IntPtr handle, JniHandleOwnership ownerShip) : base(handle, ownerShip)
        {
        }

        public override void OnCreate()
        {
            // If OnCreate is overridden, the overridden c'tor will also be called.
            base.OnCreate();
            RegisterActivityLifecycleCallbacks(this);
            Factory.Init();
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