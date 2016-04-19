using EpamVTSClient.Core.Services;
using Xamarin.Forms;

namespace XamarinEpamVTSClient.Droid
{
    public class DeviceInfoService : IDeviceInfoService
    {
        public string DeviceOs => Device.OS.ToString();
    }
}