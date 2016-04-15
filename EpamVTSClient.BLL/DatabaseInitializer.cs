using System;
using System.Threading.Tasks;
using EpamVTSClient.DAL.Models.DTOModels;
using Microsoft.Practices.Unity;
using SQLite;

namespace EpamVTSClient.BLL
{
    public static class DatabaseInitializer
    {
        public static void Initialize(IUnityContainer unityContainer)
        {
            if (unityContainer == null)
            {
                throw new ArgumentNullException(nameof(unityContainer));
            }

            var asyncConnection = unityContainer.Resolve<SQLiteAsyncConnection>();
            CreateTablesResult createTablesResult = 
                Task.Run(() => asyncConnection.CreateTablesAsync<PersonDTO, VacationDTO>()).Result;
        }
    }
}