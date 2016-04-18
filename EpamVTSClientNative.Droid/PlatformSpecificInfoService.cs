using EpamVTSClient.Core.Services;

namespace EpamVTSClientNative.Droid
{
    public class PlatformSpecificInfoService : IPlatformSpecificInfoService
    {
        public string DeviceOs => "Android";
    }
}