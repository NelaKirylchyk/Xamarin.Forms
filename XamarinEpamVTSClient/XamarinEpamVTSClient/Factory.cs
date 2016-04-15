using EpamVTSClient.BLL;
using EpamVTSClient.Core.Services;
using EpamVTSClient.DAL;
using Microsoft.Practices.Unity;

namespace XamarinEpamVTSClient
{
    public static class Factory
    {
        public static readonly IUnityContainer UnityContainer = new UnityContainer();

        public static void Init(IUnityContainerRegistry[] registries)
        {
            foreach (IUnityContainerRegistry registry in registries)
            {
                registry.Register(UnityContainer);
            }
            new UIRegistry().Register(UnityContainer);
            new DALRegistry().Register(UnityContainer);
            new BLLRegistry().Register(UnityContainer);
        }
    }
}