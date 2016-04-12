using System;
using EpamVTSClient.Core;
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
            unityContainer.RegisterType<IVacationListWebService, VacationListWebService>();

            //unityContainer.RegisterType<IConnectionManager, ConnectionManager>(new ContainerControlledLifetimeManager());

            //var connectionManager = GetInstance<IConnectionManager>();
            //var sqLiteAsyncConnection = connectionManager.GetSqLiteAsyncConnection();
            //sqLiteAsyncConnection.CreateTable<PersonDTO>();
            //sqLiteAsyncConnection.CreateTable<VacationDTO>();

            unityContainer.RegisterType<ILoginOfflineDBService, LoginOfflineDbService>();
            unityContainer.RegisterType<IVacationListOfflineDBService, VacationListOfflineDBService>();
            
        }

        protected T GetInstance<T>(string key = null)
        {
            return (T)GetInstance(typeof(T), key);
        }

        protected object GetInstance(Type service, string key)
        {
            return _unityContainer.Resolve(service, key);
        }
    }
}
