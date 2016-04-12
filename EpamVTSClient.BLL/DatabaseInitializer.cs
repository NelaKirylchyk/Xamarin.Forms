using System;
using EpamVTSClient.DAL.Models.DTOModels;
using Microsoft.Practices.Unity;
using SQLite;
using VtsMockClient.Domain.Models;

namespace EpamVTSClient.BLL
{
    public static class DatabaseInitializer
    {
        //public static async Task InitializeAsync(IUnityContainer unityContainer)
        //{
        //    if (unityContainer == null)
        //    {
        //        throw new ArgumentNullException(nameof(unityContainer));
        //    }
        //    var connection = unityContainer.Resolve<SQLiteAsyncConnection>();
        //    CreateTablesResult createTablesResult =
        //        await connection.CreateTablesAsync<Person, PersonCredentials, ShortVacationInfo, VacationInfo>();
        //}
        
        public static void Initialize(IUnityContainer unityContainer)
        {
            if (unityContainer == null)
            {
                throw new ArgumentNullException(nameof(unityContainer));
            }

            var sqlConnection = unityContainer.Resolve<SQLiteConnection>();
            sqlConnection.CreateTable(typeof(PersonDTO));
            sqlConnection.CreateTable(typeof(VacationDTO));

            //var asyncConnection = unityContainer.Resolve<SQLiteAsyncConnection>();
            //CreateTablesResult createTablesResult = Task.Run(() =>
            //    asyncConnection.CreateTablesAsync<Person, PersonCredentials, ShortVacationInfo, VacationInfo>()).Result;
        }
    }
}