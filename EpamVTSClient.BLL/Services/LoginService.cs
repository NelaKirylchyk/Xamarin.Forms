using System.Threading.Tasks;
using EpamVTSClient.DAL.Models;
using EpamVTSClient.DAL.Services;
using EpamVTSClient.DAL.Services.OfflineService;
using Plugin.Connectivity;
using VtsMockClient.Domain.Models;

namespace EpamVTSClient.BLL.Services
{
    public class LogInService : ILoginService
    {
        private readonly ILoginWebService _loginWebService;
        private readonly ILoginOfflineDBService _loginOfflineDbService;

        public LogInService(ILoginWebService loginWebService, ILoginOfflineDBService loginOfflineDbService)
        {
            _loginWebService = loginWebService;
            _loginOfflineDbService = loginOfflineDbService;
        }

        public Person User { get; private set; }

        //public bool IsConnected => CrossConnectivity.Current.IsConnected;
        public bool IsConnected => false;

        public async Task<bool> LogInAsync(string userName, string password)
        {
            if (IsConnected)
            {
                var personCredentials = new PersonCredentials()
                {
                    Email = userName,
                    Password = password
                };
                LoginResponse loginResponse = await _loginWebService.LogInAsync(personCredentials);
                if (loginResponse.LoginStatus)
                {
                    User = loginResponse.Response;
                    loginResponse.Response.Credentials = personCredentials;
                    _loginOfflineDbService.SaveUserIfNotExist(loginResponse.Response);
                }
                return loginResponse.LoginStatus;
            }
            var person = _loginOfflineDbService.SignInIfExist(userName, password);
            if (person != null)
            {
                User = person;
                return true;
            }
            return false;
        }
    }

    public interface ILoginService
    {
        Person User { get; }
        Task<bool> LogInAsync(string userName, string password);
    }
}
