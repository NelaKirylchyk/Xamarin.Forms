using Microsoft.Practices.Unity;

namespace EpamVTSClient.Core.Services
{
    public interface IUnityContainerRegistry
    {
        void Register(IUnityContainer unityContainer);
    }
}