using System.IO;
using EpamVTSClient.BLL;
using SQLite;
using XamarinEpamVTSClient.Droid;
using Environment = System.Environment;
[assembly: Xamarin.Forms.Dependency(typeof(IOSSQLiteConnectionService))]
namespace XamarinEpamVTSClient.Droid
{
    public class IOSSQLiteConnectionService : ISQLiteConnectionFactory
    {
        public SQLiteConnection GetAsyncConnection()
        {
            var fileName = "Vacations.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, fileName);

            //var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
            //var connection = new SQLite.Net.SQLiteConnection(platform, path);

            var connection = new SQLiteConnection(path);
            return connection;
        }
    }
}