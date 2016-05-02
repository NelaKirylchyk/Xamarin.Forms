using EpamVTSClient.BLL;
using EpamVTSClient.DAL;
using Microsoft.Practices.Unity;

namespace EpamVTSClientNative.iOS.Services
{
    public static class Factory
    {
        public static readonly IUnityContainer UnityContainer = new UnityContainer();

        public static void Init()
        {
            new IOSRegistry().Register(UnityContainer);
            new UIRegistry().Register(UnityContainer);
            new DALRegistry().Register(UnityContainer);
            new BLLRegistry().Register(UnityContainer);
        }
    }
}