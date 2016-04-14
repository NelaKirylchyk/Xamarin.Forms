using System.Threading.Tasks;
using EpamVTSClient.DAL.Models;

namespace EpamVTSClient.DAL.Services
{
    public interface ILoginWebService
    {
        Task<LoginResponse> LogInAsync(PersonCredentials model);
    }
}