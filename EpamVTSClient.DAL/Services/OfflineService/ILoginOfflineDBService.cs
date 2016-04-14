using System.Threading.Tasks;
using EpamVTSClient.DAL.Models;

namespace EpamVTSClient.DAL.Services.OfflineService
{
    public interface ILoginOfflineDBService
    {
        Task<Person> SignInAsync(string userName, string password);
        Task SaveUserOfflineAsync(Person person);
    }
}