namespace EpamVTSClient.Core.Services
{
    public interface IPlatformSpecificInfoService
    {
        string DeviceOs { get; }
    }
}
