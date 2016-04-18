using EpamVTSClient.Core.Services;
using Xamarin.Forms;

namespace XamarinEpamVTSClient
{
    public class PlatformSpecificInfoService : IPlatformSpecificInfoService
    {
        public string DeviceOs => Device.OS.ToString();
    }
}