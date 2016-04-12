using Microsoft.Practices.Unity;

namespace EpamVTSClient.Core
{
    public interface IUnityContainerRegistry
    {
        void Register(IUnityContainer unityContainer);
    }
}