using SQLite;

namespace EpamVTSClient.BLL
{
    public interface ISQLiteConnectionFactory
    {
        SQLiteConnection GetAsyncConnection();
    }
}
