using System;
using System.IO;
using EpamVTSClient.Core;
using Microsoft.Practices.Unity;
using SQLite;

namespace XamarinEpamVTSClient.iOS
{
    public class IOSRegistry : IUnityContainerRegistry
    {
        public void Register(IUnityContainer unityContainer)
        {
            var fileName = "Vacations.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, fileName);

            //var connection = new SQLiteAsyncConnection(path);
            var connection = new SQLiteConnection(path);
            unityContainer.RegisterInstance(connection);

            unityContainer.RegisterType<ILocalize, Localize>();
        }
    }
}
