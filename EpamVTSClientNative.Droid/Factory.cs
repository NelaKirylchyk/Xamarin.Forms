using EpamVTSClient.BLL;
using EpamVTSClient.DAL;
using EpamVTSCLient.Platform_Specific.Android;
using Microsoft.Practices.Unity;

namespace EpamVTSClientNative.Droid
{
    public static class Factory
    {
        public static readonly IUnityContainer UnityContainer = new UnityContainer();

        public static void Init()
        {
            new AndroidRegistry().Register(UnityContainer);
            new UIRegistry().Register(UnityContainer);
            new DALRegistry().Register(UnityContainer);
            new BLLRegistry().Register(UnityContainer);
        }
    }
}