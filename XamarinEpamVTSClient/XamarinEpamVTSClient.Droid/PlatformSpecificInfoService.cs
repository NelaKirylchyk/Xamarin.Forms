using EpamVTSClient.Core.Services;
using Xamarin.Forms;

namespace XamarinEpamVTSClient.Droid
{
    public class PlatformSpecificInfoService : IPlatformSpecificInfoService
    {
        public string DeviceOs => Device.OS.ToString();
    }
}