using System;
using System.IO;
using EpamVTSClient.Core.Services;
using EpamVTSClient.Core.Services.Localization;
using Microsoft.Practices.Unity;
using SQLite;

namespace EpamVTSClientNative.iOS.Services
{
    public class IOSRegistry : IUnityContainerRegistry
    {
        public void Register(IUnityContainer unityContainer)
        {
            var fileName = "Vacations.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, fileName);
            
            var connection = new SQLiteAsyncConnection(path);
            unityContainer.RegisterInstance(connection);

            unityContainer.RegisterType<ILocalize, Localize>();
        }
    }
}
