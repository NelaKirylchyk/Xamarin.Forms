using Android.App;
using Android.OS;

namespace EpamVTSClientNative.Droid
{
    [Activity(Label = "VacationListActivity")]
    public class VacationListActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.VacationList);
        }
    }
}