using SQLite;

namespace EpamVTSClient.BLL
{
    public interface ISQLiteConnectionFactory
    {
        SQLiteAsyncConnection GetAsyncConnection();
    }
}
