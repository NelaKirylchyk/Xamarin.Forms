using System.Threading.Tasks;
using Android.App;
using EpamVTSClient.Core.Services;
using Plugin.CurrentActivity;

namespace EpamVTSClientNative.Droid.Services
{
    public class MessageDialogService : IMessageDialogService
    {
        public Task ShowMessageDialogAsync(string message)
        {
            var currentActivity = CrossCurrentActivity.Current.Activity;
            AlertDialog.Builder alert = new AlertDialog.Builder(currentActivity);

            alert.SetTitle("Error");
            alert.SetMessage(message);
            alert.SetPositiveButton("OK", (senderAlert, args) => { });

            currentActivity.RunOnUiThread(() => { alert.Show(); });

            return Task.FromResult(true);
        }
    }
}