using EpamVTSClient.Core.Services;
using EpamVTSClient.DAL.Services;
using EpamVTSClient.DAL.Services.OfflineService;
using Microsoft.Practices.Unity;

namespace EpamVTSClient.DAL
{
    public class DALRegistry : IUnityContainerRegistry
    {
        private IUnityContainer _unityContainer;
        public void Register(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
            unityContainer.RegisterType<ILoginWebService, LoginWebService>();
            unityContainer.RegisterType<IVacationsWebService, VacationsWebService>();

            unityContainer.RegisterType<ILoginOfflineDBService, LoginOfflineDbService>();
            unityContainer.RegisterType<IVacationListOfflineDBService, VacationListOfflineDBService>();
        }
    }
}
