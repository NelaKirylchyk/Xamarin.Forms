using EpamVTSClient.BLL;
using SQLite;
using Xamarin.Forms;

namespace EpamVTSClient.DAL.Services.OfflineService
{
    public class ConnectionManager : IConnectionManager
    {
        private SQLiteConnection _connection;

        public  SQLiteConnection GetSqLiteAsyncConnection()
        {
            _connection = DependencyService.Get<ISQLiteConnectionFactory>().GetAsyncConnection();

            return _connection;
        }
    }

    public interface IConnectionManager
    {
        SQLiteConnection GetSqLiteAsyncConnection();
    }
}
